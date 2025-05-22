using MediatR;

namespace Cadastro.Application.UseCases.Queries.Cliente
{
    public class GetClienteByIdQuery : IRequest<Domain.Entities.Cliente>
    {
        public Guid Id { get; }

        public GetClienteByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
