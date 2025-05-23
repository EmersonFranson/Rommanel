namespace Cadastro.Domain.Entities
{
    public class Endereco
    {
        public Guid EnderecoId { get; set; } = Guid.NewGuid();
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
