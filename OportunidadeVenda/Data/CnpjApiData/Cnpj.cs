namespace OportunidadeVenda.Data.CnpjApiData
{
    using System;
    using Newtonsoft.Json;

    public partial class Cnpj
    {
       
        [JsonProperty("razao_social")]
        public string RazaoSocial { get; set; }

        public Estabelecimento Estabelecimento { get; set; }
    }

    public partial class Estabelecimento
    {
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("nome_fantasia")]
        public string NomeFantasia { get; set; }

        [JsonProperty("situacao_cadastral")]
        public string SituacaoCadastral { get; set; }

        [JsonProperty("tipo_logradouro")]
        public string TipoLogradouro { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("numero")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Numero { get; set; }

        [JsonProperty("complemento")]
        public object Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("ddd1")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Ddd1 { get; set; }

        [JsonProperty("telefone1")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Telefone1 { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("atividade_principal")]
        public Atividade AtividadePrincipal { get; set; }

        [JsonProperty("estado")]
        public Cidade Estado { get; set; }

        [JsonProperty("cidade")]
        public Cidade Cidade { get; set; }

    }

    public partial class Atividade
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("secao")]
        public string Secao { get; set; }

        [JsonProperty("divisao")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Divisao { get; set; }

        [JsonProperty("grupo")]
        public string Grupo { get; set; }

        [JsonProperty("classe")]
        public string Classe { get; set; }

        [JsonProperty("subclasse")]
        public string Subclasse { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }
    }

    public partial class Cidade
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("ibge_id")]
        public long IbgeId { get; set; }

        [JsonProperty("siafi_id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? SiafiId { get; set; }

        [JsonProperty("sigla", NullValueHandling = NullValueHandling.Ignore)]
        public string Sigla { get; set; }
    }

    public partial class InscricoesEstaduai
    {
        [JsonProperty("inscricao_estadual")]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("ativo")]
        public bool Ativo { get; set; }

        [JsonProperty("atualizado_em")]
        public DateTimeOffset AtualizadoEm { get; set; }

        [JsonProperty("estado")]
        public Cidade Estado { get; set; }
    }

    public partial class Pais
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("iso2")]
        public string Iso2 { get; set; }

        [JsonProperty("iso3")]
        public string Iso3 { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("comex_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ComexId { get; set; }
    }

    public partial class NaturezaJuridica
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }
    }

    public partial class Qualificacao
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }
    }

    public partial class Socio
    {
        [JsonProperty("cpf_cnpj_socio")]
        public string CpfCnpjSocio { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("data_entrada")]
        public DateTimeOffset DataEntrada { get; set; }

        [JsonProperty("cpf_representante_legal")]
        public string CpfRepresentanteLegal { get; set; }

        [JsonProperty("nome_representante")]
        public object NomeRepresentante { get; set; }

        [JsonProperty("faixa_etaria")]
        public string FaixaEtaria { get; set; }

        [JsonProperty("atualizado_em")]
        public DateTimeOffset AtualizadoEm { get; set; }

        [JsonProperty("pais_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PaisId { get; set; }

        [JsonProperty("qualificacao_socio")]
        public Qualificacao QualificacaoSocio { get; set; }

        [JsonProperty("qualificacao_representante")]
        public object QualificacaoRepresentante { get; set; }
    }

    internal class ParseStringConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        long l;
        if (Int64.TryParse(value, out l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (long)untypedValue;
        serializer.Serialize(writer, value.ToString());
        return;
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
}

}

