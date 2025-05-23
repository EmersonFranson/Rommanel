using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.UseCases.Commands;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Events;
using MediatR;

namespace Cadastro.Application.UseCases.Handlers
{
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateClienteCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente()
            {
                NomeRazaoSocial = request.NomeRazaoSocial,
                Documento = request.Documento,
                DataNascimento = request.DataNascimento,
                Email = request.Email,
                InscricaoEstadual = request.InscricaoEstadual,
                IsentoIE = request.IsentoIE,
                Telefone = request.Telefone,
                IsPessoaJuridica = request.IsPessoaJuridica
            };
            cliente.Endereco = new Endereco()
            {
                Logradouro = request.Endereco.Logradouro,
                Numero = request.Endereco.Numero,
                Bairro = request.Endereco.Bairro,
                Cidade = request.Endereco.Cidade,
                Estado = request.Endereco.Estado,
                CEP = request.Endereco.Cep
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync(cancellationToken);

            // Serializa os dados do cliente para armazenar no evento
            var dadosEvento = System.Text.Json.JsonSerializer.Serialize(cliente);

            // Cria o evento de criação
            var evento = new StoredEvent(
                aggregateId: cliente.ClienteId,
                tipoEvento: "ClienteCriado",
                dados: dadosEvento
            );

            _context.StoredEvents.Add(evento);
            await _context.SaveChangesAsync(cancellationToken);

            return cliente.ClienteId;
        }
    }
}
