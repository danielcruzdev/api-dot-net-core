using Newtonsoft.Json;

namespace api_dot_net_core.ViewModels.RCompras.Parametros
{
    public class ParametrosRelatorio
    {
        public int ProdutoId { get; set; }
        public int CategoriaId { get; set; }
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }

        public string ConvertModelToJson()
        {
            var jsonObj = new
            {
                ProdutoId,
                CategoriaId,
                EmpresaId,
                ClienteId
            };

            return JsonConvert.SerializeObject(jsonObj);
        }
    }
}
