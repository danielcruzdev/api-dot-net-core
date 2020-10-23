using Entity.Pessoa;
using System.Collections.Generic;

namespace Service.PessoaService
{
    public interface IPessoaService
    {
        IEnumerable<Pessoa> GetAll();
        Pessoa GetById(int id);
        bool Create(Pessoa pessoa);
        bool Update(Pessoa pessoa);
        bool Delete(int id);
    }

}