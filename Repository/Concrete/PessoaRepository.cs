using api_dot_net_core.Models;
using Entity.Pessoa;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.PessoaRepository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly CoreDbContext _context;
        public PessoaRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            IEnumerable<Pessoa> pessoas = await _context.Pessoa.Select(p => new Pessoa
            {
                Id = p.Id,
                Nome = p.Nome,
                Sobrenome = p.Sobrenome
            }).ToListAsync();

            return pessoas;
        }
        public async Task<Pessoa> GetByIdAsync(int id)
        {
            Pessoa pessoa = await _context.Pessoa
              .Select(p => new Pessoa
              {
                  Id = p.Id,
                  Nome = p.Nome,
                  Sobrenome = p.Sobrenome
              }).Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return pessoa;
        }
        public async Task<bool> CreateAsync(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                _context.Pessoa.Add(pessoa);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(Pessoa pessoa)
        {
            if (pessoa != null)
            {
                _context.Pessoa.Update(pessoa);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAsync(int id)
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
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }

}