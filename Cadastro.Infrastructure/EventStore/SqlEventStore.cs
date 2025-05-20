using Cadastro.Domain.Events; 
using Cadastro.Domain.Interfaces;
using Cadastro.Infrastructure;
using System.Text.Json;

public class SqlEventStore : IEventStore
{
    private readonly AppDbContext _context;

    public SqlEventStore(AppDbContext context)
    {
        _context = context;
    }

    public async Task SalvarEvento<T>(T evento) where T : IEvent
    {
        var serializedData = JsonSerializer.Serialize(evento, evento.GetType());

        var storedEvent = new StoredEvent(
            evento.ClienteId,
            evento.GetType().Name,
            serializedData
        );

        _context.StoredEvents.Add(storedEvent);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> ObterEventos<T>(Guid aggregateId) where T : IEvent
    {
        var events = _context.StoredEvents
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.DataOcorrencia)
            .ToList();

        var result = new List<T>();

        foreach (var evt in events)
        {
            var deserialized = JsonSerializer.Deserialize<T>(evt.Dados);
            if (deserialized != null)
                result.Add(deserialized);
        }

        return result;
    }
}
