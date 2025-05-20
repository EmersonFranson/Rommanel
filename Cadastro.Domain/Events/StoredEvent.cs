namespace Cadastro.Domain.Events
{
    public class StoredEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string TipoEvento { get; set; }
        public string Dados { get; set; } // JSON
        public DateTime DataOcorrencia { get; set; }

        public StoredEvent() { }

        public StoredEvent(Guid aggregateId, string tipoEvento, string dados)
        {
            Id = Guid.NewGuid();
            AggregateId = aggregateId;
            TipoEvento = tipoEvento;
            Dados = dados;
            DataOcorrencia = DateTime.UtcNow;
        }
    }
}
