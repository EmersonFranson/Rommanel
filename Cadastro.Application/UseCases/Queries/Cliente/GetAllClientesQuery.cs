using MediatR;

namespace Cadastro.Application.UseCases.Queries.Cliente
{
    public class GetAllClientesQuery : IRequest<IEnumerable<Domain.Entities.Cliente>>
    {
    }
}
