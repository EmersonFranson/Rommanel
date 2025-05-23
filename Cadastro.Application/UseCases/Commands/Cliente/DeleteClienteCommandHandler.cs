using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.UseCases.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
