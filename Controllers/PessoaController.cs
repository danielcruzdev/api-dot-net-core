using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.PessoaService;
using System.Collections.Generic;
using ViewModel.Pessoa;

namespace api_dot_net_core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;
        public PessoaController(IMapper mapper, IPessoaService pessoaService)
        {

            _mapper = mapper;
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pessoa = _pessoaService.GetAllPeople();
            var viewModel = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoa);

            return Ok(viewModel);
        }
    }
}
