using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OportunidadeVenda.Data.CnpjApiData.Services;
using OportunidadeVenda.Data.CnpjApiData;
using OportunidadeVenda.Context;
using OportunidadeVenda.Data;
using System.Text.RegularExpressions;
using System.Text.Json.Nodes;

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
        public async Task<Cnpj> GetCnpj(string cnpj)
        {
            //Instaciando Classe CNPJ API SERVICE
            CnpjApiService cnpjApiService = new CnpjApiService();

            //REMOVER MASK STRING CNPJ
            Regex numeros = new Regex(@"[^0-9]");

            /* TESTE DEPURADOR */
            System.Diagnostics.Debug.WriteLine($"REGEX CNPJ ------ {numeros.Replace(cnpj, "")}");

            //Deserializando e retornando os atributos da API
            Cnpj cnpjInfo = await cnpjApiService.Informacao(numeros.Replace(cnpj, ""));

            return cnpjInfo;

            

        }

        // GET: api/Cnpj/Oportonidade/Buscar/{usuario} - Buscar Oportunidades com base na região do usuário
        [HttpGet("/Oportunidades/Buscar/{id_usuario}")]
        public async Task<ActionResult<List<Cnpj>>> GetOpotunidadeBuscar(int id_usuario)
        {
            
            //recupera lista
            var listCnpj = new List<Cnpj>();

            //Seleciona um vendedor&
            UsuariosController usuariosController = new(_context);
            var usuario = await usuariosController.GetUsuario(id_usuario);

            //Recebendo lista de oportunidade de acordo com a regiao do usuário
            OportunidadesController oportunidadeController = new(_context);
            var oportunidade = await oportunidadeController.GetOportunidade();

            /* TESTE DEPURADOR */
            System.Diagnostics.Debug.WriteLine($"USER REGIAO ------ {usuario?.Value?.Name}");

            foreach (Oportunidade opt in oportunidade.Value)
            {
                
                //get atributos da API CNPJ
                Cnpj cnpj = await GetCnpj(opt.Cnpj);

                /* TESTE DEPURADOR */
                System.Diagnostics.Debug.WriteLine($"OPT CNPJ ------ {opt.Cnpj}");
              
                //Realizando calculo para obter valor correspondente a região do usuáro
                var regiaocnpj_int = cnpj.Estabelecimento?.Estado.IbgeId;
                var regiaocnpj = cnpj.Estabelecimento?.Estado.IbgeId.ToString();
                int regiaoCase = Convert.ToInt32(regiaocnpj?.Substring(1, 1));
                int regiaousuario = (int)((usuario?.Value?.Regiao * 10) + regiaoCase);

                /* TESTE DEPURADOR */
                System.Diagnostics.Debug.WriteLine($"API CNPJ INT ------ {cnpj.Estabelecimento?.Estado.IbgeId}");
                System.Diagnostics.Debug.WriteLine($"API CNPJ RAZAO ------ {cnpj.RazaoSocial}");

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



