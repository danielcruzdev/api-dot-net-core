using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dot_net_core.Entities.RCompras
{
    public class RCompraProduto
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int ValorAgregadoId { get; set; }
        public string ValorDesc { get; set; }
    }
}
