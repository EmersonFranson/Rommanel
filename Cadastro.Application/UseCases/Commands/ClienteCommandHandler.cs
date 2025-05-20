using Cadastro.Application.Common.Interfaces.Persistence;
using MediatR;

namespace Cadastro.Application.UseCases.Commands
{
    public class ClienteCommandHandler : IRequestHandler<ClienteCommandRequest, ClienteCommandResponse>
    {
        private readonly IAppDbContext _appDbContext;

        public ClienteCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ClienteCommandResponse> Handle(ClienteCommandRequest request, CancellationToken cancellationToken)
        {
            ////Validar paginação
            //if (request.PageSize > 1000)
            //    throw new RetornoException(nameof(UpdatePatrimoniaCommandResponse), Erros.ObterErro(ErroType.TamanhoPaginaSuperiorAoPermitido), HttpStatusCode.UnprocessableEntity);

            //var response = AtualizarDadosCotacao(request, cancellationToken);

            var response = new ClienteCommandResponse();

            return await Task.FromResult(response);
        }
    }
}
