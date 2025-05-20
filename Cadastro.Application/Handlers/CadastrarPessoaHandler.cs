using Cadastro.Application.EventSourcing;
using Cadastro.Application.Interfaces;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Events;
using FluentValidation;
using MediatR;

namespace Cadastro.Application.Handlers
{
    // Application/Commands/CadastrarClienteHandler.cs
    public class CadastrarPessoaHandler : IRequestHandler<CadastrarPessoaCommand, Guid>
    {
        private readonly IPessoaRepository _repository;
        private readonly IEventStore _eventStore;

        public CadastrarPessoaHandler(IPessoaRepository repository, IEventStore eventStore)
        {
            _repository = repository;
            _eventStore = eventStore;
        }

        public async Task<Guid> Handle(CadastrarPessoaCommand request, CancellationToken cancellationToken)
        {
            // Verificações duplicadas (e-mail, cpf/cnpj)
            if (await _repository.ExistePorCpfCnpj(request.CpfCnpj) || await _repository.ExistePorEmail(request.Email))
                throw new ValidationException("Cliente já cadastrado.");

            var cliente = new Pessoa();
            await _repository.Adicionar(cliente);

            // Event Sourcing
            var evento = new ClienteCadastradoEvent(cliente.Id,"Razao Social", "cpf","email", DateTime.Now);
            await _eventStore.SalvarEvento(evento);

            return cliente.Id;
        }
    }

}
