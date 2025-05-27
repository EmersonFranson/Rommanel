using MediatR;

namespace Cadastro.Application.UseCases.Commands
{
    public class UpdateClienteCommand : IRequest<bool>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NomeRazaoSocial { get; set; }
        public string Documento { get; set; } // CPF ou CNPJ
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool IsPessoaJuridica { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool IsentoIE { get; set; }
        public EnderecoCommand Endereco { get; set; }
    }
}
