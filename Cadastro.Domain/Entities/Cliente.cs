namespace Cadastro.Domain.Entities
{
    public class Cliente 
    {
        public Guid ClienteId { get;  set; } = Guid.NewGuid();
        public string NomeRazaoSocial { get;  set; }
        public string Documento { get;  set; }
        public DateTime? DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool IsPessoaJuridica { get; set; }        
        public string? InscricaoEstadual { get; set; }
        public bool IsentoIE { get; set; }
        public bool IsDeleted { get; set; }
        public Endereco Endereco { get; set; }
    }
}
