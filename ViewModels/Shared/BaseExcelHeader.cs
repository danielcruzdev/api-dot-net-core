using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_dot_net_core.ViewModels.Shared
{
    public abstract class BaseExcelHeader
    {
        public string NomeFantasia { get; set; }
        public string UsuarioLogin { get; set; }
        public string ContentType { get; set; }
        public byte[] Logo { get; set; }
        public DateTime DataImpressao { get; set; }
    }
}
