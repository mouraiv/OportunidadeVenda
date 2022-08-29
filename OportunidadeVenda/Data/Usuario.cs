

using Microsoft.Build.Framework;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace OportunidadeVenda.Data
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Email { get; set; }
        public int Regiao { get; set; }
        public Collection<Oportunidade>? Oportunidades { get; private set; }
    }
}
