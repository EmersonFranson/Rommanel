using Cadastro.Domain.Entities;
using Cadastro.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
