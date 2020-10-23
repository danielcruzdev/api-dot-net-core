using AutoMapper;
using Entity.Pessoa;
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
        public IActionResult ListAll()
        {
            var pessoa = _pessoaService.GetAll();
            var viewModel = _mapper.Map<IEnumerable<PessoaViewModel>>(pessoa);

            return Ok(viewModel);
        }

        [HttpGet("{id:int}")]
        public IActionResult ListById([FromRoute] int id)
        {
            var pessoa = _pessoaService.GetById(id);
            var viewModel = _mapper.Map<PessoaViewModel>(pessoa);

            return Ok(viewModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Pessoa pessoa)
        {
            var result = _pessoaService.Create(pessoa);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Pessoa pessoa)
        {
            var result = _pessoaService.Update(pessoa);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _pessoaService.Delete(id);

            if (result)
                return Ok();
            else
                return BadRequest();
        }
    }
}
