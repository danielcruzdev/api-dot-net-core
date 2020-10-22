using System;
using System.Collections.Generic;
using Repository.PessoaRepository;

namespace Service.PessoaService
{
    public class PessoaService : IPessoaService
    {
        private IList<Pessoa> _people;
        private IPessoaRepository _pessoaRepository;

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