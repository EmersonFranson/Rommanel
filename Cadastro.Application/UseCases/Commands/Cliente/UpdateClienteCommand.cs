using MediatR;
using System;

namespace Cadastro.Application.UseCases.Commands
{
    public class UpdateClienteCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
