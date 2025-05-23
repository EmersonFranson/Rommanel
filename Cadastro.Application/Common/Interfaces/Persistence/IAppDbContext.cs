using Cadastro.Domain.Entities;
using Cadastro.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Application.Common.Interfaces.Persistence
{
    public interface IAppDbContext
    {
        DbSet<Domain.Entities.Cliente> Clientes { get; set; }
        DbSet<Endereco> Enderecos { get; set; }
        DbSet<StoredEvent> StoredEvents { get; set; }

        // Métodos CRUD para Cliente
        Task<List<Domain.Entities.Cliente>> BuscarTodosClientesAsync(CancellationToken cancellationToken = default);
        Task<Domain.Entities.Cliente?> BuscarClientePorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Domain.Entities.Cliente> AdicionarClienteAsync(Domain.Entities.Cliente cliente, CancellationToken cancellationToken = default);
        Task<Domain.Entities.Cliente?> AtualizarClienteAsync(Domain.Entities.Cliente clienteAtualizado, CancellationToken cancellationToken = default);
        Task<bool> RemoverClienteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
