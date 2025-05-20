using Cadastro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Domain.Events
{
    // Domain/Events/ClienteCadastradoEvent.cs
    public record ClienteCadastradoEvent(Guid ClienteId, string NomeRazaoSocial, string CpfCnpj, string Email, DateTime DataCadastro) : IEvent;

}
