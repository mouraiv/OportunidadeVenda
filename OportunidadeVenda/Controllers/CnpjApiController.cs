using Microsoft.AspNetCore.Mvc;
using OportunidadeVenda.Data.CnpjApiData.Services;
using OportunidadeVenda.Data.CnpjApiData;
using OportunidadeVenda.Context;
using OportunidadeVenda.Data;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace OportunidadeVenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CnpjApiController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICnpjApiService _cnpjApiService;

        public CnpjApiController(AppDbContext context, ICnpjApiService cnpjApiService)
        {
            _context = context;
            _cnpjApiService = cnpjApiService;
        }

        // GET: api/Cnpj/{cnpj} - API Externa tendo como base informações do IBGE
        [HttpGet("{cnpj}")]
        public async Task<Cnpj> GetCnpj(string cnpj)
        {

            //REMOVER MASK STRING CNPJ
            Regex numeros = new Regex(@"[^0-9]");

            /* TESTE DEPURADOR */
            System.Diagnostics.Debug.WriteLine($"REGEX CNPJ ------ {numeros.Replace(cnpj, "")}");

            //Deserializando e retornando os atributos da API
            Cnpj cnpjInfo = await _cnpjApiService.Informacao(numeros.Replace(cnpj, ""));

            return cnpjInfo;
        }

        // GET: api/Cnpj/Oportonidade/Buscar/{usuario} - Buscar Oportunidades com base na região do usuário
        [HttpGet("/Oportunidades/Buscar/{id_usuario}")]
        public async Task<ActionResult<List<Cnpj>>> GetOpotunidadeBuscar(int id_usuario)
        {
            
            //recupera lista
            var listCnpj = new List<Cnpj>();

            //Seleciona um vendedor
            var usuario = await _context.Usuarios.FindAsync(id_usuario);

            //Carregar oportunidades
            var oportunidade = await _context.Oportunidade.ToListAsync();

            foreach (Oportunidade opt in oportunidade)
            {
                //get atributos da API CNPJ
                Cnpj cnpj = await _cnpjApiService.Informacao(opt.Cnpj);
              
                //Realizando calculo para obter valor correspondente a região do usuáro
                var regiaocnpj_int = cnpj.Estabelecimento?.Estado.IbgeId;
                var regiaocnpj = cnpj.Estabelecimento?.Estado.IbgeId.ToString();
                int regiaoCase = Convert.ToInt32(regiaocnpj?.Substring(1, 1));
                int regiaousuario = (int)((usuario.Regiao * 10) + regiaoCase);

                //Lista Cnpj por região do usuário 
                if (regiaocnpj_int == regiaousuario && opt.IdUsuario == null) {
                            
                    //adicionar lista
                    listCnpj.Add(cnpj);
                }
             }
            //retornar lista
            return listCnpj;
        }

    }
}



