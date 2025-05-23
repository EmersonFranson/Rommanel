using Cadastro.Domain.Entities;
using MediatR;

namespace Cadastro.Application.UseCases.Commands 
{
    public class ClienteCommandRequest : IRequest<ClienteCommandResponse>
    {
        public Guid Id { get;  set; }
        public string NomeRazaoSocial { get;  set; }
        public string CpfCnpj { get;  set; }
        public DateTime DataNascimento { get;  set; }
        public string Telefone { get; set; }
        public string Email { get;  set; }
        public Endereco Endereco { get;  set; }
        public string TipoPessoa { get; set; } // "F" ou "J"
        public string? InscricaoEstadual { get; set; }
        public bool IsentoIE { get; set; }
    }
}
