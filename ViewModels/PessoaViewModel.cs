using Newtonsoft.Json;

namespace ViewModel
{
    public class PessoaViewModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string ConvertModelToJSON(int? id)
        {
            var jsonObj = new
            {
                Id = id,
                Nome,
                Sobrenome
            };

            return JsonConvert.SerializeObject(jsonObj);
        }

    }
}
