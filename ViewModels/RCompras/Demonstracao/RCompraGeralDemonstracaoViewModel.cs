using System.Collections.Generic;

namespace api_dot_net_core.ViewModels.RCompras.Demonstracao
{
    public class RCompraGeralDemonstracaoViewModel
    {
        public RCompraGeralDemonstracaoViewModel()
        {
            Compras = new List<RComprasDemonstracaoViewModel>();
        }

        public RCompraCabecalhoViewModel Cabecalho { get; set; }
        public List<RComprasDemonstracaoViewModel> Compras { get; set; }
    }
}
