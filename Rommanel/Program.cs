using Cadastro.Application.Common.Interfaces.Persistence;
using Cadastro.Application.Common.Validators.Cliente;
using Cadastro.Application.UseCases.Commands;
using Cadastro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Cadastro.Infrastructure.Persistence;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ⛓️ Add Controllers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();

// 📦 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Cadastro de Cliente",
        Version = "v1"
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// 🔌 Entity Framework + SQL Server (use Docker Compose para rodar o banco)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔁 Registro do contexto no container de injeção
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

// 💬 MediatR - Registra comandos, handlers, etc.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(ClienteCommandRequest).Assembly
));

builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// 🌐 Swagger UI
app.UseSwagger(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
