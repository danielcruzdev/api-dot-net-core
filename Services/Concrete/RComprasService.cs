using api_dot_net_core.Entities.RCompras;
using api_dot_net_core.Repository.Contract;
using api_dot_net_core.Services.Contract;
using System;
using System.Threading.Tasks;

namespace api_dot_net_core.Services.Concrete
{
    public class RComprasService : IRComprasService
    {
        private readonly IRCompraRepository _rCompraRepository;

        public RComprasService(IRCompraRepository rCompraRepository)
        {
            _rCompraRepository = rCompraRepository ?? throw new ArgumentNullException(nameof(rCompraRepository));
        }

        public async Task<RCompra> ReportData(string tabParametros)
        {
            return await _rCompraRepository.ReportData(tabParametros);
        }
    }
}
