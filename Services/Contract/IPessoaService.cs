using System.Collections.Generic;
using Entity.Pessoa;

namespace Service.PessoaService
{
    public interface IPessoaService
    {
        bool AddPerson(Pessoa pessoa);
        IEnumerable<Pessoa> GetAllPeople();
    }

}