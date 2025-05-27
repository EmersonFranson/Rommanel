using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.UseCases.Commands;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            cliente.Endereco = await _context.Enderecos
    .FirstOrDefaultAsync(x => x.ClienteId == cliente.ClienteId, cancellationToken);


            if (cliente == null) return false;

           var response = AtualizarCliente(cliente,request);

            //_context.Clientes.Add(cliente);
            await _context.AtualizarClienteAsync(response, cancellationToken);

            // Serializa os dados do cliente para armazenar no evento
            var dadosEvento = System.Text.Json.JsonSerializer.Serialize(cliente);

            // Cria o evento de criação
            var evento = new StoredEvent(
                aggregateId: cliente.ClienteId,
                tipoEvento: "ClienteAtualizado",
                dados: dadosEvento
            );

            _context.StoredEvents.Add(evento);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        private Cliente AtualizarCliente(Cliente cliente, UpdateClienteCommand request)
        {
            cliente.NomeRazaoSocial = request.NomeRazaoSocial;
            cliente.Telefone = request.Telefone;
            cliente.Email = request.Email;
            cliente.DataNascimento = request.DataNascimento;
            cliente.Documento = request.Documento;
            cliente.IsPessoaJuridica = request.IsPessoaJuridica;
            cliente.InscricaoEstadual = request.InscricaoEstadual;
            cliente.IsentoIE = request.IsentoIE;
            cliente.Endereco.Logradouro = request.Endereco.Logradouro;
            cliente.Endereco.Numero = request.Endereco.Numero;
            cliente.Endereco.Bairro = request.Endereco.Bairro;  
            cliente.Endereco.Cidade = request.Endereco.Cidade;
            cliente.Endereco.Estado = request.Endereco.Estado;
            cliente.Endereco.CEP = request.Endereco.Cep;

            return cliente;
        }
    }
}
