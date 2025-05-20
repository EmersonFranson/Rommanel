using Application.Responses;
using Cadastro.Application.EventSourcing;
using MediatR;

public class CriarClienteCommandHandler : IRequestHandler<CadastrarPessoaCommand, CommandResponse>
{
    private readonly AppDbContext _context;
    private readonly IEventStore _eventStore;

    public CriarClienteCommandHandler(AppDbContext context, IEventStore eventStore)
    {
        _context = context;
        _eventStore = eventStore;
    }

    public async Task<CommandResponse> Handle(CadastrarPessoaCommand request, CancellationToken cancellationToken)
    {
        // Verificar duplicidade de CPF/CNPJ ou e-mail
        bool existeCpf = await _context.Clientes.AnyAsync(c => c.CpfCnpj == request.CpfCnpj);
        if (existeCpf)
            return CommandResponse.Falha("Já existe um cliente com este CPF/CNPJ.");

        bool existeEmail = await _context.Clientes.AnyAsync(c => c.Email == request.Email);
        if (existeEmail)
            return CommandResponse.Falha("Já existe um cliente com este e-mail.");

        // Pessoa Física: Verificar idade mínima
        if (!request.IsPessoaJuridica)
        {
            int idade = DateTime.Today.Year - request.DataNascimento.Year;
            if (request.DataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;

            if (idade < 18)
                return CommandResponse.Falha("A idade mínima para cadastro de pessoa física é 18 anos.");
        }
        else
        {
            // Pessoa Jurídica: IE obrigatório se não for isento
            if (!request.IsentoIE && string.IsNullOrWhiteSpace(request.InscricaoEstadual))
                return CommandResponse.Falha("Pessoa jurídica deve informar IE ou marcar como isento.");
        }

        var cliente = new Cliente(
            request.NomeRazaoSocial,
            request.CpfCnpj,
            request.Email,
            request.Telefone,
            request.DataNascimento,
            request.IsPessoaJuridica ? TipoPessoa.Juridica : TipoPessoa.Fisica,
            request.InscricaoEstadual,
            request.IsentoIE
        );

        cliente.DefinirEndereco(request.Cep, request.Endereco, request.Numero, request.Bairro, request.Cidade, request.Estado);

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync(cancellationToken);

        // Criar evento de domínio e salvar no EventStore
        var evento = new ClienteCriadoEvent(cliente.Id, cliente.NomeRazaoSocial, cliente.CpfCnpj);
        await _eventStore.SalvarEvento(evento);

        return CommandResponse.Ok("Cliente criado com sucesso.");
    }
}
