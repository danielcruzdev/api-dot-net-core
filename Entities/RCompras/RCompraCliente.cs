using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dot_net_core.Entities.RCompras
{
    public class RCompraCliente
    {
        public RCompraCliente()
        {
            Empresas = new List<RCompraEmpresa>();
        }

        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteTelefone { get; set; }
        public List<RCompraEmpresa> Empresas { get; set; }
    }
}
