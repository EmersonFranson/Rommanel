namespace Cadastro.Domain.Interfaces
{
    public interface IEvent
    {
        Guid ClienteId { get; }
        string NomeRazaoSocial { get; }
        string CpfCnpj { get; }
        string Email { get; }
        DateTime DataCadastro { get; }
    }
}
