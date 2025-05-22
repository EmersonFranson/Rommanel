using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.UseCases.Commands;
using Cadastro.Domain.Entities;
using MediatR;

namespace Cadastro.Application.UseCases.Handlers
{
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateClienteCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente()
            {
                NomeRazaoSocial = request.NomeRazaoSocial,
                CpfCnpj = request.Documento,
                DataNascimento = request.DataNascimento,
                Email = request.Email,
                InscricaoEstadual = request.InscricaoEstadual,
                IsentoIE = request.IsentoIE,
                Telefone = request.Telefone,
                TipoPessoa = VerificarTipoPessoa(request.Documento),
                Endereco = new Endereco()
            };
            cliente.Endereco = request.Endereco;          

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync(cancellationToken);

            return cliente.Id;
        }
        private string VerificarTipoPessoa(string documento)
        {
            documento = new string(documento.Where(char.IsDigit).ToArray());

            if (documento.Length == 11 && EhCpfValido(documento))
                return "PF";

            if (documento.Length == 14 && EhCnpjValido(documento))
                return "PJ";

            return "Inválido";
        }
        private bool EhCpfValido(string cpf)
        {
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            var digito = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            digito = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(tempCpf[9].ToString() + digito);
        }

        private bool EhCnpjValido(string cnpj)
        {
            if (cnpj.Length != 14 || cnpj.All(c => c == cnpj[0]))
                return false;

            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCnpj = cnpj.Substring(0, 12);
            var soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            var digito = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            digito = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(tempCnpj[12].ToString() + digito);
        }


    }
}
