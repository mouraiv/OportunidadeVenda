namespace OportunidadeVenda.Data.CnpjApiData.Services
{
    public interface ICnpjApiService
    {
        public Task<Cnpj> Informacao(string cnpj);
    }
}
