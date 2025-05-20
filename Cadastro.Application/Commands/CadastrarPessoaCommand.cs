using MediatR;
using Application.Responses;

public class CadastrarPessoaCommand : IRequest<CommandResponse>
{
    public string NomeRazaoSocial { get; set; }
    public string CpfCnpj { get; set; }
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; }

    public string Cep { get; set; }
    public string Endereco { get; set; }
    public string Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }

    public bool IsPessoaJuridica { get; set; }
    public string? InscricaoEstadual { get; set; }
    public bool IsentoIE { get; set; }
}
