using api_dot_net_core.Services.Contract;
using api_dot_net_core.ViewModels.RCompras;
using api_dot_net_core.ViewModels.RCompras.Parametros;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api_dot_net_core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RCompraController : Controller
    {
        private readonly IRComprasService _rComprasService;
        private readonly IMapper _mapper;

        public RCompraController(IMapper mapper, IRComprasService rComprasService)
        {
            _mapper = mapper;
            _rComprasService = rComprasService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RCompraViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportData([FromQuery] ParametrosRelatorio parametros)
        {
            var tabParametros = parametros.ConvertModelToJson();

            var relatorio = await _rComprasService.ReportData(tabParametros);
            var viewModel = _mapper.Map<RCompraViewModel>(relatorio);

            return Ok(viewModel);
        }
    }
}
