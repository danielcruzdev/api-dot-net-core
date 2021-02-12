using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dot_net_core.ViewModels.RCompras
{
    public class RCompraClienteViewModel
    {
        public RCompraClienteViewModel()
        {
            Empresas = new List<RCompraEmpresaViewModel>();
        }

        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteTelefone { get; set; }
        public List<RCompraEmpresaViewModel> Empresas { get; set; }
    }
}
