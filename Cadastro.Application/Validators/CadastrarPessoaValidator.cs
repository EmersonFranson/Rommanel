using Cadastro.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Application.Validators
{
    // Application/Validators/CadastrarClienteValidator.cs
    public class CadastrarPessoaValidator : AbstractValidator<CadastrarPessoaCommand>
    {
        public CadastrarPessoaValidator()
        {
            RuleFor(x => x.NomeRazaoSocial).NotEmpty();
            RuleFor(x => x.CpfCnpj).NotEmpty().Must(IsValidCpfCnpj);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Telefone).NotEmpty();

            When(x => x.TipoPessoa == "F", () =>
            {
                RuleFor(x => x.DataNascimento)
                    .Must(data => data <= DateTime.Today.AddYears(-18))
                    .WithMessage("Cliente deve ter pelo menos 18 anos.");
            });

            When(x => x.TipoPessoa == "J", () =>
            {
                RuleFor(x => x.IsentoIE)
                    .Must((cmd, isento) => isento || !string.IsNullOrEmpty(cmd.InscricaoEstadual))
                    .WithMessage("IE é obrigatório se não for isento.");
            });

            // Endereço
            RuleFor(x => x.Endereco).SetValidator(new EnderecoValidator());
        }

        private bool IsValidCpfCnpj(string cpfCnpj) => true; // TODO: implementar validação real
    }

}
