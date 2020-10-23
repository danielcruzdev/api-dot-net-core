using Entity.Pessoa;
using System.Collections.Generic;

namespace Repository.PessoaRepository
{
    public interface IPessoaRepository
    {
        IEnumerable<Pessoa> GetAll();
        Pessoa GetById(int id);
        bool Create(Pessoa pessoa);
        bool Update(Pessoa pessoa);
        bool Delete(int id);
    }

}