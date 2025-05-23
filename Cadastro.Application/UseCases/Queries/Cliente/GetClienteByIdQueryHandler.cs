using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.UseCases.Queries.Cliente;
using Cadastro.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Application.UseCases.Handlers
{
    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, Cliente>
    {
        private readonly IAppDbContext _context;

        public GetClienteByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clientes
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.ClienteId == request.Id, cancellationToken);
        }
    }
}
