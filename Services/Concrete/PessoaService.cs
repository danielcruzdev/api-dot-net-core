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
        public IEnumerable<Pessoa> GetAll()
        {
            return _pessoaRepository.GetAll();

        }
        public Pessoa GetById(int id)
        {
            return _pessoaRepository.GetById(id);
        }

        public bool Create(Pessoa pessoa)
        {
            return _pessoaRepository.Create(pessoa);

        }

        public bool Update(Pessoa pessoa)
        {
            return _pessoaRepository.Update(pessoa);

        }

        public bool Delete(int id)
        {
            return _pessoaRepository.Delete(id);

        }
    }

}