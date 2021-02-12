using api_dot_net_core.Entities.RCompras;
using api_dot_net_core.Entities.RCompras.Demonstracao;
using System.Threading.Tasks;

namespace api_dot_net_core.Services.Contract
{
    public interface IRComprasService
    {
        Task<RCompra> ReportData(string tabParametros);
        Task<RCompraGeralDemonstracao> ReportWrongData(string tabParametros);
    }
}
