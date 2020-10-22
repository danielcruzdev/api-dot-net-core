using System.Collections.Generic;

namespace Repository.PessoaRepository
{
    public class PessoaRepository : IPessoaRepository
    {
        private IList<Pessoa> _people;
        public PessoaRepository()
        {
            _people = new List<Pessoa>();
        }

        public bool AddPerson(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                _people.Add(pessoa);
                return true;
            }
            return false;
        }

        public IEnumerable<Pessoa> GetAllPeople()
        {
            return _people;
        }
    }

}