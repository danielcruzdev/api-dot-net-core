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

        public IEnumerable<Pessoa> GetAll()
        {
            IEnumerable<Pessoa> pessoas = _context.Pessoa.Select(p => new Pessoa
            {
                Id = p.Id,
                Nome = p.Nome,
                Sobrenome = p.Sobrenome
            }).ToList();

            return pessoas;
        }
        public Pessoa GetById(int id)
        {
            Pessoa pessoa = _context.Pessoa
              .Select(p => new Pessoa
              {
                  Id = p.Id,
                  Nome = p.Nome,
                  Sobrenome = p.Sobrenome
              }).Where(p => p.Id == id)
                .FirstOrDefault();

            return pessoa;
        }

        public bool Create(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                _context.Pessoa.Add(pessoa);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                _context.Pessoa.Update(pessoa);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            if (id != null)
            {
                var pessoa = _context.Pessoa.Select(a =>
                new Pessoa
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Sobrenome = a.Sobrenome
                }).Where(a => a.Id == id)
                  .FirstOrDefault();

                _context.Pessoa.Remove(pessoa);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }

}