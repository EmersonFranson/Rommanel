using MediatR;

namespace Cadastro.Application.UseCases.Commands
{
    public class DeleteClienteCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteClienteCommand(Guid id)
        {
            Id = id;
        }
    }
}
