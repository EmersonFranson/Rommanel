using Cadastro.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StoredEventConfiguration : IEntityTypeConfiguration<StoredEvent>
{
    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        builder.ToTable("StoredEvents");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.TipoEvento)
               .HasMaxLength(250)
               .IsRequired();

        builder.Property(e => e.Dados)
               .IsRequired();

        builder.Property(e => e.DataOcorrencia)
               .IsRequired();
    }
}
