using Cadastro.Domain.Interfaces;

namespace Cadastro.Application.Interfaces.Interfaces
{
    public interface IEventStore
    {
        Task SalvarEvento<T>(T evento) where T : IEvent;
        Task<IEnumerable<T>> ObterEventos<T>(Guid aggregateId) where T : IEvent;
    }
}
