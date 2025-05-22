using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }

        // CRUD Methods

        public async Task<List<Cliente>> BuscarTodosClientesAsync(CancellationToken cancellationToken = default)
        {
            return await Clientes
                .Include(c => c.Endereco)
                .ToListAsync(cancellationToken);
        }

        public async Task<Cliente?> BuscarClientePorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Cliente> AdicionarClienteAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {
            await Clientes.AddAsync(cliente, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return cliente;
        }

        public async Task<Cliente?> AtualizarClienteAsync(Cliente clienteAtualizado, CancellationToken cancellationToken = default)
        {
            var clienteExistente = await Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == clienteAtualizado.Id, cancellationToken);

            if (clienteExistente == null)
                return null;

            Entry(clienteExistente).CurrentValues.SetValues(clienteAtualizado);

            if (clienteAtualizado.Endereco != null)
            {
                Entry(clienteExistente.Endereco).CurrentValues.SetValues(clienteAtualizado.Endereco);
            }

            await SaveChangesAsync(cancellationToken);
            return clienteExistente;
        }

        public async Task<bool> RemoverClienteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cliente = await Clientes.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (cliente == null)
                return false;

            Clientes.Remove(cliente);
            await SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.NomeRazaoSocial)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.CpfCnpj).IsUnique();

                entity.HasOne(c => c.Endereco)
                      .WithOne()
                      .HasForeignKey<Endereco>(e => e.ClienteId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Cep)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(e => e.Logradouro)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Numero)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.Bairro)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Cidade)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Estado)
                      .IsRequired()
                      .HasMaxLength(50);
            });
        }
    }
}
