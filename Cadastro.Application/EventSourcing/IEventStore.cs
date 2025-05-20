using Cadastro.Domain.Events;

namespace Cadastro.Application.EventSourcing
{
    public interface IEventStore
    {
        Task SalvarEvento(ClienteCadastradoEvent evento);
    }
}
