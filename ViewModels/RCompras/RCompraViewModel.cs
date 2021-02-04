using System.Collections.Generic;

namespace api_dot_net_core.ViewModels.RCompras
{
    public class RCompraViewModel
    {
        public RCompraViewModel()
        {
            Clientes = new List<RCompraClienteViewModel>();
        }

        public RCompraCabecalhoViewModel Cabecalho { get; set; }
        public List<RCompraClienteViewModel> Clientes { get; set; }
    }
}
