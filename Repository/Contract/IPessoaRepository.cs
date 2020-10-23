using Entity.Pessoa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.PessoaRepository
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<Pessoa> GetByIdAsync(int id);
        Task<bool> CreateAsync(Pessoa pessoa);
        Task<bool> UpdateAsync(Pessoa pessoa);
        Task<bool> DeleteAsync(int id);
    }

}