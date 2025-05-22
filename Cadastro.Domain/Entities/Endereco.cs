﻿namespace Cadastro.Domain.Entities
{
    public class Endereco
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }       
    }
}
