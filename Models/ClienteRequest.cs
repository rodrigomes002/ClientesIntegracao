namespace ClientesIntegracao.Models
{
    public class ClienteRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string TokenEgestor { get; set; }
        public string TokenMovidesk { get; set; }
    }
}
