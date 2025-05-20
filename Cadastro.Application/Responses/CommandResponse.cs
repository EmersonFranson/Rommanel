// Application/Responses/CommandResponse.cs
namespace Application.Responses;

public class CommandResponse
{
    public bool Sucesso { get; set; }
    public string? Mensagem { get; set; }

    public static CommandResponse Ok(string? mensagem = null) => new() { Sucesso = true, Mensagem = mensagem };
    public static CommandResponse Falha(string mensagem) => new() { Sucesso = false, Mensagem = mensagem };
}
