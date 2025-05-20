using MediatR;

namespace Cadastro.Application.UseCases.Commands 
{
    public class ClienteCommandRequest : IRequest<ClienteCommandResponse>
    {
        public string? Nome { get; set; }
    }
}
