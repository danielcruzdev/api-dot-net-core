using Entity.Pessoa;
using System.Collections.Generic;

namespace Repository.PessoaRepository
{
    public interface IPessoaRepository
    {
        bool AddPerson(Pessoa pessoa);
        IEnumerable<Pessoa> GetAllPeople();
        Pessoa GetById(int id);
    }

}