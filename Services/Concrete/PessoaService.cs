using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
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
        public async Task<int> CreateAsync(string parametrosJson)
        {
            return await _pessoaRepository.CreateAsync(parametrosJson);
        }
        public async Task<int> UpdateAsync(string parametrosJson)
        {
            return await _pessoaRepository.UpdateAsync(parametrosJson);
        }
        public async Task<bool> DeleteAsync(int? id)
        {
            return await _pessoaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> GetAllActiveAsync()
        {
            return await _pessoaRepository.GetAllActiveAsync();
        }
    }

}