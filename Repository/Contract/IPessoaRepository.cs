using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<IEnumerable<Pessoa>> GetAllActiveAsync();
        Task<Pessoa> GetByIdAsync(int id);
        Task<int> CreateAsync(string parametrosJson);
        Task<int> UpdateAsync(string parametrosJson);
        Task<bool> DeleteAsync(int? id);
    }

}