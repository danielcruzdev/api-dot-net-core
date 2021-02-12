using System;

namespace api_dot_net_core.ViewModels.Shared
{
    public class BaseExcelViewModel
    {
        public string UsuarioLogin { get; set; }
        public string ContentType { get; set; }
        public byte[] Logo { get; set; }
        public DateTime DataImpressao { get; set; }
    }
}
