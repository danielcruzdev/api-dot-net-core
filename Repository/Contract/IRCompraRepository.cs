using api_dot_net_core.Entities.RCompras;
using api_dot_net_core.Entities.RCompras.Demonstracao;
using System.Threading.Tasks;

namespace api_dot_net_core.Repository.Contract
{
    public interface IRCompraRepository
    {
        Task<RCompra> ReportData(string tabParametros);
        Task<RCompraGeralDemonstracao> ReportWrongData(string tabParametros);
    }
}
