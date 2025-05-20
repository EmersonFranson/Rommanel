using Cadastro.Domain.Entities;

namespace Cadastro.Application.Interfaces
{
    internal interface IPessoaRepository
    {
        Task Adicionar(Pessoa cliente);
        Task<bool> ExistePorCpfCnpj(string cpfCnpj);
        Task<bool> ExistePorEmail(string email);
    }
}
