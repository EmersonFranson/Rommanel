using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.UseCases.Queries.Cliente;
using Cadastro.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Application.UseCases.Handlers
{
    public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, IEnumerable<Cliente>>
    {
        private readonly IAppDbContext _context;

        public GetAllClientesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clientes
                .Include(c => c.Endereco)
                .ToListAsync(cancellationToken);
        }
    }
}
