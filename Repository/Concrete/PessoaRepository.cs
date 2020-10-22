using api_dot_net_core.Models;
using Entity.Pessoa;
using System.Collections.Generic;
using System.Linq;

namespace Repository.PessoaRepository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly CoreDbContext _context;
        public PessoaRepository(CoreDbContext context)
        {
            _context = context;
        }

        public bool AddPerson(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                _context.Pessoa.Add(pessoa);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Pessoa> GetAllPeople()
        {
            IEnumerable<Pessoa> pessoas = _context.Pessoa.Select(p => new Pessoa
            {
                Id = p.Id,
                Nome = p.Nome,
                Sobrenome = p.Sobrenome
            }).ToList();

            return pessoas;
        }
    }

}