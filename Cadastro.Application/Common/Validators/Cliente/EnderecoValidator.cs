using FluentValidation;

namespace Cadastro.Application.Common.Validators.Cliente
{
    public class EnderecoValidator : AbstractValidator<string?>
    {
        public EnderecoValidator()
        {
            RuleFor(query => query)
                .NotNull()
                .NotEmpty()
                .WithMessage("Endereço inválido!");
        }
    }
}
