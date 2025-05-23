using Cadastro.Domain.Entities;
using MediatR;
using System;

namespace Cadastro.Application.UseCases.Commands
{
    public class CreateClienteCommand : IRequest<Guid>
    {
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

    public class EnderecoCommand
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
    }
}
