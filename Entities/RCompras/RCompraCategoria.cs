using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dot_net_core.Entities.RCompras
{
    public class RCompraCategoria
    {
        public RCompraCategoria()
        {
            Produtos = new List<RCompraProduto>();
        }

        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public List<RCompraProduto> Produtos { get; set; }
    }
}
