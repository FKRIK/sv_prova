using System.Collections.Generic;
using System.Linq;
using API_Folhas.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Folhas.Controllers
{
    [ApiController]
    [Route("api/folha")]
    public class FolhaController : ControllerBase
    {
        private readonly DataContext _context;
         public FolhaController(DataContext context) => _context = context;

         // GET - Listar todas as folhas
         // Rota - /api/folha/listar
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            return Ok(_context.Folhas.ToList());
        }

        // GET - Listar uma folha
        // Rota: /api/folha/{cpf}/{mes}/{ano}
        [HttpGet]
        [Route("buscar/{cpf}/{mes}/{ano}")]
        public IActionResult ListarUma([FromRoute] string cpf, string mes, string ano)
        {
            Folha folha = _context.Folhas.FirstOrDefault(a => a.Mes == mes && a.Ano == ano && a.CpfFunc == cpf);
            Funcionario funcionario = _context.Funcionarios.FirstOrDefault(f=> f.Cpf == folha.CpfFunc);
            folha.funcionario = funcionario;
            return Ok(folha);
        }


        // GET - Listar folhas do mes
        // Rota: /api/folha/{mes}/{ano}
        [HttpGet]
        [Route("filtrar/{mes}/{ano}")]
        public IActionResult ListarMes(string mes, string ano)
        {

            List<Folha> folhas = new List<Folha>();
            var busca = _context.Folhas.ToList().Where(f=> f.Mes.Equals(mes) && f.Ano.Equals(ano));
                for(int i =0; i < busca.Count(); i++){
                    busca.ElementAt(1).funcionario = _context.Funcionarios.FirstOrDefault(f=> f.Cpf == busca.ElementAt(i).CpfFunc);
            }
            return Ok(busca.ToList());
        }


        // POST - Cadastrar uma folha
        // Rota - /api/folha/cadastrar
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Folha folha)
        {
           Funcionario funcionaio = _context.Funcionarios.FirstOrDefault(f=> f.Cpf == folha.CpfFunc);
           Folha folhaoutra = new Folha(funcionaio);
            _context.Folhas.Add(folhaoutra);
            _context.SaveChanges();
            return Created("", folhaoutra);
        }
    }
}