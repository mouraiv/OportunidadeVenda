using Newtonsoft.Json;
using OportunidadeVenda.Controllers;

namespace OportunidadeVenda.Data.CnpjApiData.Services
{
    public class CnpjApiService
    {
        public async Task<Cnpj> Informacao(string cnpj)
        {

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://publica.cnpj.ws/cnpj/{cnpj}");
            var jsonString = await response.Content.ReadAsStringAsync();

            //Deserialização e restaurando os atributos do objeto
            var _jsonObject = JsonConvert.DeserializeObject<Cnpj>(jsonString);

            //Retornando o Objeto
            return _jsonObject;

        }
    }
}
