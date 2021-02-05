using api_dot_net_core.ViewModels.Shared;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace api_dot_net_core.Api.Infrastructure.Helpers
{
    public static class FileHelpers
    {
        public static async Task<UploadContentViewModel> ProcessStreamedFile(IEnumerable<UploadViewModel> contents, long sizeLimit)
        {
            var result = new UploadContentViewModel();
            var errors = new List<UploadErrorViewModel>();
            var success = new List<MidiaContentViewModel>();

            foreach (var content in contents)
            {
                var problemDetails = new UploadErrorViewModel
                {
                    Id = content.Id,
                    Title = "Erro ao enviar arquivo."
                };

                if (content.File == null)
                {
                    problemDetails.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    problemDetails.Detail = $"Arquivo com ID {content.Id} está vazio.";
                    errors.Add(problemDetails);

                    continue;
                }

                bool hasErrors = false;
                var fileName = content.File.FileName;

                using var memoryStream = new MemoryStream();
                await content.File.CopyToAsync(memoryStream);

                if (memoryStream.Length == 0)
                {
                    hasErrors = true;
                    problemDetails.Detail = $"Arquivo {fileName} está vazio.";
                    problemDetails.StatusCode = StatusCodes.Status422UnprocessableEntity;
                }
                else if (memoryStream.Length > sizeLimit)
                {
                    var megabyteSizeLimit = sizeLimit / 1048576;
                    hasErrors = true;
                    problemDetails.Detail = $"O arquivo {fileName} excedeu tamanho limite de {megabyteSizeLimit:N1} MB.";
                    problemDetails.StatusCode = StatusCodes.Status413PayloadTooLarge;
                }

                if (hasErrors)
                {
                    errors.Add(problemDetails);
                }
                else
                {
                    success.Add(new MidiaContentViewModel
                    {
                        Guid = content.Id,
                        Name = fileName,
                        Content = memoryStream.ToArray(),
                        ContentType = content.File.ContentType
                    });
                }
            }

            result.FilesWithErrors = errors;
            result.SuccessFiles = success;

            return result;
        }

    }
}
