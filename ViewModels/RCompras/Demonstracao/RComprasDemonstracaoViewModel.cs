namespace api_dot_net_core.ViewModels.RCompras.Demonstracao
{
    public class RComprasDemonstracaoViewModel
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteTelefone { get; set; }
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public string EmpresaTelefone { get; set; }
        public string EmpresaCidade { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int ValorAgregadoId { get; set; }
        public string ValorDesc { get; set; }
    }
}
