using System.Collections.Generic;

namespace api_dot_net_core.Entities.RCompras
{
    public class RCompra
    {
        public RCompra()
        {
            Clientes = new List<RCompraCliente>();
        }

        public RCompraCabecalho Cabecalho { get; set; }
        public List<RCompraCliente> Clientes { get; set; }
    }
}
