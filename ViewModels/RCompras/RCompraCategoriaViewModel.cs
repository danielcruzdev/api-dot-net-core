using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dot_net_core.ViewModels.RCompras
{
    public class RCompraCategoriaViewModel
    {
        public RCompraCategoriaViewModel()
        {
            Produtos = new List<RCompraProdutoViewModel>();
        }

        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public List<RCompraProdutoViewModel> Produtos { get; set; }
    }
}
