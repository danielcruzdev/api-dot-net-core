using api_dot_net_core.Controllers;
using api_dot_net_core.ViewModels.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace api_dot_net_core.Intraestructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ExceptionMiddleware(RequestDelegate next, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _next = next;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var languageResources = new Dictionary<string, string>
            {
                { "erro_inesperado_processar_dados_detalhes", _sharedLocalizer.GetString("erro_inesperado_processar_dados_detalhes") },
                { "erro_inserir_registro", _sharedLocalizer.GetString("erro_inserir_registro") },
                { "erro_excluir_registro", _sharedLocalizer.GetString("erro_excluir_registro") },
            };

            var problemDetails = new ErrorViewModel
            {
                Instance = context.Request.HttpContext.Request.Path,
                Title = languageResources["erro_inesperado_processar_dados_detalhes"],
                StatusCode = StatusCodes.Status500InternalServerError,
                Detail = exception.Message,
                Method = context.Request.HttpContext.Request.Method
            };

            if (exception is SqlException sqlException)
            {
                var (title, detailMsg, isExpected) = TrataMsgErroBanco(languageResources, sqlException.Message);
                if (!string.IsNullOrEmpty(title))
                    problemDetails.Title = title;

                problemDetails.Detail = detailMsg;
                problemDetails.IsExpected = isExpected;
            }

            context.Response.StatusCode = problemDetails.StatusCode;
            context.Response.ContentType = "application/problem+json";

            var error = JsonConvert.SerializeObject(problemDetails);
            return context.Response.WriteAsync(error);
        }

        private (string title, string detailMsg, bool isExpected) TrataMsgErroBanco(Dictionary<string, string> languageResources, string msg)
        {
            try
            {
                if (msg.IndexOf("erro_banco", StringComparison.Ordinal) > -1)
                {
                    var title = "";

                    var retornoErroBanco = new Dictionary<int, string> {
                        { 13003, languageResources["erro_inesperado_processar_dados_detalhes"] },
                        { 13004, languageResources["erro_inserir_registro"] },
                        { 13005, languageResources["erro_excluir_registro"] },
                        { 13006, languageResources["erro_inesperado_processar_dados_detalhes"] }
                    };

                    var indexOf = msg.IndexOf("<erro_banco>", StringComparison.Ordinal);
                    var lastIndexOf = msg.LastIndexOf("</erro_banco>", StringComparison.Ordinal) + 1;
                    var msgXml = msg.Substring(indexOf, lastIndexOf);

                    var idXml = Regex.Match(msgXml, @"<id>\s*(.+?)\s*</id>");
                    idXml = Regex.Match(idXml.ToString(), @"\d+");
                    var idXmlNumber = int.Parse(idXml.ToString());

                    if (retornoErroBanco.ContainsKey(idXmlNumber))
                    {
                        title = retornoErroBanco[idXmlNumber];
                    }

                    var doc = XDocument.Parse(msgXml);
                    var objErr = doc.Elements("erro_banco").Select(x => new
                    {
                        mensagem = $"{x.Element("id").Value} - {x.Element("mensagem").Value}"
                    }).FirstOrDefault().mensagem;

                    if (msgXml.Contains("parametros"))
                    {
                        var paramsList = new List<string>();

                        var objParam = doc.Elements("erro_banco")
                            .Select(x => new
                            {
                                parametros = x.Element("parametros").Elements("parametro")
                            })
                            .FirstOrDefault();

                        paramsList.AddRange(objParam.parametros.Select(element => element.Value));
                        msgXml = string.Format(objErr, paramsList.ToArray());
                    }
                    else
                    {
                        title = objErr;
                        msgXml = "";
                    }

                    return (title, msgXml, false);
                }
                else if (msg.IndexOf("ERROR_OBJECT", StringComparison.Ordinal) > -1)
                {
                    return (null, msg, false);
                }
                else if (msg.IndexOf("CHECK constraint", StringComparison.Ordinal) > -1)
                {
                    return (languageResources["erro_inesperado_processar_dados_detalhes"], msg, false);
                }
                return (null, msg, false);
            }
            catch
            {
                return (null, msg, false);
            }

        }
    }
}
