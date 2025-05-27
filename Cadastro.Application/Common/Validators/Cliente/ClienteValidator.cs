using Cadastro.Application.UseCases.Commands;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Cadastro.Application.Common.Validators.Cliente
{
    public class ClienteValidator : AbstractValidator<CreateClienteCommand?>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.NomeRazaoSocial)
                .NotEmpty().WithMessage("Nome ou Razão Social é obrigatório.")
                .MaximumLength(200);

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage("CPF ou CNPJ é obrigatório.")
                .Must(IsValidCpfOrCnpj).WithMessage("CPF ou CNPJ inválido.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório.")
                .MaximumLength(20);

            When(c => IsCpf(c.Documento), () =>
            {
                RuleFor(c => c.DataNascimento)
                    .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                    .Must(data => CalcularIdade(data) >= 18)
                    .WithMessage("Idade mínima para CPF é de 18 anos.");
            });

            When(c => IsCnpj(c.Documento), () =>
            {
                RuleFor(c => c.InscricaoEstadual)
                    .NotEmpty().When(c => !c.IsentoIE)
                    .WithMessage("Informe a Inscrição Estadual ou marque como isento.");
            });
        }

        private static bool IsCpf(string documento)
        {
            return documento.Length <= 11;
        }

        private static bool IsCnpj(string documento)
        {
            return documento.Length > 11;
        }

        private static int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        private static bool IsValidCpfOrCnpj(string documento)
        {
            documento = Regex.Replace(documento, "[^0-9]", "");

            if (documento.Length == 11)
                return ValidarCpf(documento);
            else if (documento.Length == 14)
                return ValidarCnpj(documento);
            return false;
        }

        private static bool ValidarCpf(string cpf)
        {
            if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
                return false;

            var soma1 = 0;
            var soma2 = 0;
            for (int i = 0; i < 9; i++)
            {
                soma1 += (cpf[i] - '0') * (10 - i);
                soma2 += (cpf[i] - '0') * (11 - i);
            }

            var digito1 = soma1 % 11;
            digito1 = digito1 < 2 ? 0 : 11 - digito1;
            soma2 += digito1 * 2;

            var digito2 = soma2 % 11;
            digito2 = digito2 < 2 ? 0 : 11 - digito2;

            return cpf[9] - '0' == digito1 && cpf[10] - '0' == digito2;
        }

        private static bool ValidarCnpj(string cnpj)
        {
            if (cnpj.Length != 14 || new string(cnpj[0], 14) == cnpj)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            tempCnpj += resto;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(resto.ToString());
        }
    }
}
