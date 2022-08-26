using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OportunidadeVenda.Data.CnpjApiData.Services;
using OportunidadeVenda.Data.CnpjApiData;
using System.Text.RegularExpressions;
using OportunidadeVenda.Context;
using OportunidadeVenda.Data;

namespace OportunidadeVenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CnpjApiController : Controller
    {
        private readonly AppDbContext _context;

        public CnpjApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cnpj/{cnpj} - API Externa tendo como base informações do IBGE
        [HttpGet("{cnpj}")]
        public async Task<List<Cnpj>> GetCnpj(string cnpj)
        {
                //Instaciando Classe CNPJ API SERVICE
                CnpjApiService cnpjApiService = new CnpjApiService();

                //Deserializando e retornando os atributos da API
                Cnpj cnpjInfo = await cnpjApiService.Informacao(cnpj);
                
                var listCnpj = new List<Cnpj>();
                listCnpj.Add(cnpjInfo);
                return listCnpj;

        }

        // GET: api/Cnpj/Oportonidade/Buscar/{usuario} - Buscar Oportunidades com base na região do usuário
        [HttpGet("/Oportunidades/Buscar/{id_usuario}")]
        public async Task<List<Object>> GetOpotunidadeBuscar(int id_usuario)
        {
            //Seleciona um vendedor    
            var usuario = await _context.Usuarios.Where(p => p.Id == id_usuario).ToListAsync();

            //Recebendo lista de oportunidade de acordo com a regiao do usuário
            var oportunidade = await _context.Oportunidade.ToListAsync();

            //Instaciando Classe CNPJ API SERVICE
            CnpjApiService cnpjApiService = new CnpjApiService();

            //recupera lista
            var listCnpj = new List<Object>();

            foreach (var opt in oportunidade)
            {
                //get atributos da API CNPJ
                Cnpj cnpjInfo = await cnpjApiService.Informacao(opt.Cnpj);

                //Realizando calculo para obter valor correspondente a região do usuáro
                //ainda sendo feito
                
                /* TODO */

                //Lista Cnpj por região do usuário - (ainda sendo feito)
                if (cnpjInfo.Estabelecimento.Estado.IbgeId == 35) {
                    //adicionar lista
                    listCnpj.Add(cnpjInfo);
                }
            }
            //retornar lista
            return listCnpj;

        }
    }
}



