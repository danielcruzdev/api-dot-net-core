using Newtonsoft.Json;
using System;

namespace ViewModel
{
    public class PessoaViewModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public bool HabCarro { get; set; }
        public bool HabMoto { get; set; }
        public bool Ativo { get; set; }

        public string ConvertModelToJSON(int? id)
        {
            var jsonObj = new
            {
                Id = id,
                Nome,
                Sobrenome,
                Email,
                Telefone,
                DataNascimento,
                Cpf,
                Rg,
                HabCarro,
                HabMoto,
                Ativo

            };

            return JsonConvert.SerializeObject(jsonObj);
        }

    }
}
