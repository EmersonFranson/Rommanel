using FluentValidation;

namespace Cadastro.Application.Common.Validators.Cliente
{
    public class ClienteValidator : AbstractValidator<string?>
    {
        public ClienteValidator()
        {
            RuleFor(query => query)
                .NotNull()
                .NotEmpty()
                .WithMessage("Identificação inválida!");
        }
    }
}
