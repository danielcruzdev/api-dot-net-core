using Entity.Pessoa;
using Repository.PessoaRepository;
using System;
using System.Collections.Generic;

namespace Service.PessoaService
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository ?? throw new ArgumentNullException(nameof(pessoaRepository));
        }

        public bool AddPerson(Pessoa pessoa)
        {
            return _pessoaRepository.AddPerson(pessoa);
        }

        public IEnumerable<Pessoa> GetAllPeople()
        {
            return _pessoaRepository.GetAllPeople();
        }
    }

}