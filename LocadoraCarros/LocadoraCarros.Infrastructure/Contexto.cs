using LocadoraCarros.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Infrastructure
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
