using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dot_net_core.Entities.RCompras
{
    public class RCompraEmpresa
    {
        public RCompraEmpresa()
        {
            Categorias = new List<RCompraCategoria>();
        }

        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public string EmpresaTelefone { get; set; }
        public string EmpresaCidade { get; set; }
        public List<RCompraCategoria> Categorias { get; set; }
    }
}
