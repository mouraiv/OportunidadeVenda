
using OportunidadeVenda.Data.CnpjApiData;
using System.Text.Json.Serialization;

namespace OportunidadeVenda.Data
{
    public class Oportunidade
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public decimal Valor { get; set; }
        public int? IdUsuario { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }

    }
}
