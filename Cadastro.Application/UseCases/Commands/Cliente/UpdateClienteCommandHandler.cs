using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.UseCases.Commands;
using MediatR;

namespace Cadastro.Application.UseCases.Handlers
{
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, bool>
    {
        private readonly IAppDbContext _context;

        public UpdateClienteCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Clientes.FindAsync(new object[] { request.Id }, cancellationToken);

            if (cliente == null) return false;

            //cliente.Atualizar(request.NomeRazaoSocial, request.Telefone, request.Email);

            //cliente.Endereco?.Atualizar(request.Cep, request.Logradouro, request.Numero, request.Bairro, request.Cidade, request.Estado);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
