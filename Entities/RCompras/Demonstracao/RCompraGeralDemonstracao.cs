using System.Collections.Generic;

namespace api_dot_net_core.Entities.RCompras.Demonstracao
{
    public class RCompraGeralDemonstracao
    {
        public RCompraGeralDemonstracao()
        {
            Compras = new List<RComprasDemonstracao>();
        }

        public RCompraCabecalho Cabecalho { get; set; }
        public List<RComprasDemonstracao> Compras { get; set; }
    }
}
