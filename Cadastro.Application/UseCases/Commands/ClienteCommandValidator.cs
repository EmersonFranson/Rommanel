using Cadastro.Application.Common.Validators.Cliente;
using FluentValidation;

namespace Cadastro.Application.UseCases.Commands
{
    public class ClienteCommandValidator : AbstractValidator<ClienteCommandRequest>
    {
        public ClienteCommandValidator()
        {
            //RuleFor(query => query.NomeRazaoSocial).NotNull().SetValidator(new ClienteValidator());
        }        
    }
}
