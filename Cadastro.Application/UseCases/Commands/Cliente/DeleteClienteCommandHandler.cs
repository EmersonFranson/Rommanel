using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.UseCases.Commands;
using Cadastro.Domain.Events;
using MediatR;

namespace Cadastro.Application.UseCases.Handlers
{
    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteClienteCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Clientes.FindAsync(new object[] { request.Id }, cancellationToken);
            if (cliente == null) return false;

            cliente.IsDeleted = true; // Marca o cliente como deletado

            await _context.AtualizarClienteAsync(cliente, cancellationToken);

            // Serializa os dados do cliente para armazenar no evento
            var dadosEvento = System.Text.Json.JsonSerializer.Serialize(cliente);

            // Cria o evento de criação
            var evento = new StoredEvent(
                aggregateId: cliente.ClienteId,
                tipoEvento: "ClienteDeletado",
                dados: dadosEvento
            );

            _context.StoredEvents.Add(evento);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
