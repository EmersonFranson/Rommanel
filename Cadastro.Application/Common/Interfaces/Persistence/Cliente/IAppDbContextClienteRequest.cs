using Cadastro.Application.Common.Interfaces.Repositories;
using Cadastro.Domain.Entities;

namespace Cadastro.Application.Common.Interfaces.Persistence.Cliente
{
    public interface IApplicationDbContextQuotesRequest : IAppDbContext, ISqlRepository<Domain.Entities.Cliente> { }
}
