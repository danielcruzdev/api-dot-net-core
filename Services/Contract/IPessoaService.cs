using System.Collections.Generic;

namespace Service.PessoaService
{
    public interface IPessoaService
    {
        bool AddPerson(Pessoa pessoa);
        IEnumerable<Pessoa> GetAllPeople();
    }

}