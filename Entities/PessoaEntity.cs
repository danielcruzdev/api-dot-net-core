using System;

namespace Entity
{
    public class Pessoa
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
    }
}
