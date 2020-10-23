using AutoMapper;
using Entity.Pessoa;
using Microsoft.AspNetCore.Mvc;
using Service.PessoaService;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> ListAllAsync()
        {
            var pessoa = await _pessoaService.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoa);

            return Ok(viewModel);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ListByIdAsync([FromRoute] int id)
        {
            var pessoa = await _pessoaService.GetByIdAsync(id);
            var viewModel = _mapper.Map<PessoaViewModel>(pessoa);

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Pessoa pessoa)
        {
            var result = await _pessoaService.CreateAsync(pessoa);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Pessoa pessoa)
        {
            var result = await _pessoaService.UpdateAsync(pessoa);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _pessoaService.DeleteAsync(id);

            if (result)
                return Ok();
            else
                return BadRequest();
        }
    }
}