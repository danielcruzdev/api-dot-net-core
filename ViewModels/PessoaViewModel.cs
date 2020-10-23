using Newtonsoft.Json;

namespace ViewModel
{
    public class PessoaViewModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }


        public string ConvertModelToJSON()
        {
            var jsonObj = new
            {
                Id,
                Nome,
                Sobrenome
            };

            return JsonConvert.SerializeObject(jsonObj);
        }

    }
}
