using System.Collections.Generic;
using Entity.Pessoa;

namespace Repository.PessoaRepository
{
    public interface IPessoaRepository
    {
        bool AddPerson(Pessoa pessoa);
        IEnumerable<Pessoa> GetAllPeople();
    }

}