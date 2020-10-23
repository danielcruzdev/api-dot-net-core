using Entity.Pessoa;
using Microsoft.EntityFrameworkCore;

namespace api_dot_net_core.Models
{
    public partial class CoreDbContext : DbContext
    {
        public virtual DbSet<Pessoa> Pessoa { get; set; }

        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=FINLANDIA;Database=API_TESTE;Initial Catalog=API_TESTE; Integrated Security=True;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
