using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dot_net_core.ViewModels.RCompras
{
    public class RCompraEmpresaViewModel
    {
        public RCompraEmpresaViewModel()
        {
            Categorias = new List<RCompraCategoriaViewModel>();
        }

        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public string EmpresaTelefone { get; set; }
        public string EmpresaCidade { get; set; }
        public List<RCompraCategoriaViewModel> Categorias { get; set; }
    }
}
