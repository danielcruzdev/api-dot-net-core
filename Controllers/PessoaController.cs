using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace api_dot_net_core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;
        private readonly string[] _languageResources;

        public PessoaController(IMapper mapper,
            IPessoaService pessoaService,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _mapper = mapper;
            _pessoaService = pessoaService;

            _languageResources = new string[1]
            {
                "Teste"
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PessoaViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListAllAsync()
        {
            var pessoa = await _pessoaService.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoa);

            return Ok(viewModel);
        }

        [HttpGet("ativo")]
        [ProducesResponseType(typeof(IEnumerable<PessoaViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListAllActiveAsync()
        {
            var pessoa = await _pessoaService.GetAllActiveAsync();
            var viewModel = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoa);

            return Ok(viewModel);
        }

        [HttpGet("{id:int}", Name = nameof(ListByIdAsync))]
        [ProducesResponseType(typeof(PessoaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListByIdAsync([FromRoute] int id)
        {
            var pessoa = await _pessoaService.GetByIdAsync(id);
            var viewModel = _mapper.Map<PessoaViewModel>(pessoa);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PessoaViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] PessoaViewModel pessoa)
        {
            var parametrosJson = pessoa.ConvertModelToJSON(null);
            int pessoaId = await _pessoaService.CreateAsync(parametrosJson);

            return CreatedAtRoute(
                nameof(ListByIdAsync),
                new { id = pessoaId },
                pessoa
            );
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PessoaViewModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id,
                                                     [FromBody] PessoaViewModel pessoa)
        {
            var parametrosJson = pessoa.ConvertModelToJSON(id);
            int pessoaId = await _pessoaService.UpdateAsync(parametrosJson);

            return CreatedAtRoute(
                nameof(ListByIdAsync),
                new { id = pessoaId },
                pessoa
            );
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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