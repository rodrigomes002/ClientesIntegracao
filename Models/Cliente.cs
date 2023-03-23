using Amazon.DynamoDBv2.DataModel;

namespace ClientesIntegracao.Models
{
    [DynamoDBTable("clientes")]
    public class Cliente
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("nome")]
        public string Nome { get; set; }

        [DynamoDBProperty("cnpj")]
        public string Cnpj { get; set; }

        [DynamoDBProperty("tokenEgestor")]
        public string TokenEgestor { get; set; }

        [DynamoDBProperty("tokenMovidesk")]
        public string TokenMovidesk { get; set; }

        public Cliente() { }

        public Cliente(string nome, string cnpj, string tokenEgestor, string tokenMovidesk)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Cnpj = cnpj;
            TokenEgestor = tokenEgestor;
            TokenMovidesk = tokenMovidesk;
        }
    }
}
