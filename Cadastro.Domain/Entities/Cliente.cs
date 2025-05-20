namespace Cadastro.Domain.Entities
{
    public class Cliente 
    {
        public Guid Id { get; private set; }
        public string NomeRazaoSocial { get; private set; }
        public string CpfCnpj { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public Endereco Endereco { get; private set; }
        public string TipoPessoa { get; private set; } // "F" ou "J"
        public string? InscricaoEstadual { get; private set; }
        public bool IsentoIE { get; private set; }
    }
}
