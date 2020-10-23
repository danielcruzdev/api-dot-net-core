using Entity.Pessoa;
using Repository.PessoaRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.PessoaService
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository ?? throw new ArgumentNullException(nameof(pessoaRepository));
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _pessoaRepository.GetAllAsync();
        }
        public async Task<Pessoa> GetByIdAsync(int id)
        {
            return await _pessoaRepository.GetByIdAsync(id);
        }
        public async Task<bool> CreateAsync(Pessoa pessoa)
        {
            return await _pessoaRepository.CreateAsync(pessoa);
        }
        public async Task<bool> UpdateAsync(Pessoa pessoa)
        {
            return await _pessoaRepository.UpdateAsync(pessoa);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _pessoaRepository.DeleteAsync(id);
        }
    }

}