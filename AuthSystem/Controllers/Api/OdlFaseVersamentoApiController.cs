using AuthSystem.ApiModel;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthSystem.Controllers.Api
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OdlFaseVersamentoApiController : ControllerBase
    {
        private readonly NContext _context;

        public OdlFaseVersamentoApiController(NContext context)
        {
            _context = context;
        }
        // GET: api/<OdlFaseVersamentoApiController>
        [HttpGet]
        public IEnumerable<OdlFaseVersamento> Get()
        {
            return _context.OdlFaseVersamenti;
        }

        // GET api/<OdlFaseVersamentoApiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var versamento = await _context.OdlFaseVersamenti
                .FirstOrDefaultAsync(m => m.IdVersamento == id);
            if (versamento == null)
            {
                return NotFound();
            }

            return this.Ok(versamento);
        }

        

        //POST api/<OdlFaseVersamentoApiController>
        //[HttpPost]
        //[ActionName("PostControlloUno")]
        //public async Task<IActionResult> Post([FromBody] OdlFaseVersamento utente)
        //{
        //    ApplicationUser user = new ApplicationUser { UserName = utente.Email, Email = utente.Email, FirstName = utente.FirstName, LastName = utente.LastName };

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    _context.Add(utente);
        //    await _context.SaveChangesAsync();

        //    return this.Ok(utente);
        //}

        //POST api/<OdlFaseVersamento1ApiController>
        [HttpPost]
        [ActionName("PostControlloDue")]
        public async Task<IActionResult> PostControlloDue(int codiceOdl, int nomeFase, OdlFaseVersamento versamento)
        {
            //prende un ordine di lavoro in base al codice che gli viene passato
            var odl = await _context.Odls
                .SingleOrDefaultAsync(m => m.CodiceOdl == codiceOdl);

            var fase = await _context.OdlFasi
                .SingleOrDefaultAsync(m => (int)m.Fase == nomeFase && m.CodiceOdl == codiceOdl);

            var versamentiFasePrecedente = _context.OdlFaseVersamenti.Where(m => m.Fasi.CodiceOdl == codiceOdl && (int)m.Fasi.Fase == (nomeFase - 1)).ToList();

            var versamentiFaseAttuale = _context.OdlFaseVersamenti.Where(m => m.Fasi.CodiceOdl == codiceOdl && (int)m.Fasi.Fase == nomeFase).ToList();

            var pezziBuoniFasePrecedente = versamentiFasePrecedente.Sum(s => s.PezziBuoni);

            var pezziBuoniFaseAttuale = versamentiFaseAttuale.Sum(s => s.PezziBuoni);

            

            //controllo se la fase è F10
            if (nomeFase == 1)
            {
                //controllo se i pezzi inseriti non superano la quantià totale dell'odl
                if ((pezziBuoniFaseAttuale + versamento.PezziBuoni) > odl.QuantitaDaProdurre)
                {
                    return Ok(new ApiResult<OdlFaseVersamento>()
                    {
                        Ok = false,
                        Message = "stai inserendo una quantità superiore a quella dell'odl"
                    });
                    //BadRequest("Cattiva Camilla, stai inserendo una quantità superiore a quella dell'odl");

                }
                else
                {
                    _context.OdlFaseVersamenti.Add(versamento);
                    await _context.SaveChangesAsync();
                    return Ok(versamento);
                }

            }
            else   //se la fase è diversa dalla fase 10
            {
                //controllo se esistono versamenti nella fase precedente alla fase attuale
                if(pezziBuoniFasePrecedente == 0)
                {
                    return Ok(new ApiResult<OdlFaseVersamento>()
                    {
                        Ok = false,
                        Message = "non puoi fare versamenti in questa fase poichè non esistono versamenti precedenti"
                    });
                    //return BadRequest("Cattiva Camilla, non puoi fare versamenti in questa fase poichè non esistono versamenti precedenti");
                }
                //controllo se i pezzi inseriti in questo versamento superano i pezzi versati nella fase precedente
                if (versamento.PezziBuoni > pezziBuoniFasePrecedente - pezziBuoniFaseAttuale)
                {
                    return Ok(new ApiResult<OdlFaseVersamento>()
                    {
                        Ok = false,
                        Message = "stai versando una quantità di pezzi superiore al numero di pezzi disponibili dalla fase precedente"
                    });
                    //return BadRequest("Cattiva Camilla, stai versando una quantità di pezzi superiore al numero di pezzi disponibili dalla fase precedente");
                }
                else
                {
                    _context.OdlFaseVersamenti.Add(versamento);
                    await _context.SaveChangesAsync();
                    //controllo se il versamento attuale va a completare l'odl
                    var fasiOdl = _context.OdlFasi.Where(m => m.CodiceOdl == codiceOdl ).ToList();
                    var conteggioFasi = fasiOdl.Count();
                    var pezziBuoniFaseAttuale2 = versamentiFaseAttuale.Sum(s => s.PezziBuoni);
                    pezziBuoniFaseAttuale2 = pezziBuoniFaseAttuale2 + versamento.PezziBuoni;
                    if (nomeFase == conteggioFasi && odl.QuantitaDaProdurre == pezziBuoniFaseAttuale2)
                    {
                        return Ok(new ApiResult<OdlFaseVersamento>()
                        {
                            Ok = true,
                            Message = "Hai completato l'odl: " + odl.CodiceOdl
                        });
                        //return this.Ok("Hai completato l'odl: " + odl.CodiceOdl);
                    }

                    return Ok(new ApiResult<OdlFaseVersamento>()
                    {
                        Ok = true,
                        DataResult = versamento
                    });

                }

            }
        }

            //[HttpPost]
            //[ActionName("PostControlloTre")]
            //public async Task<IActionResult> PostControlloTre([FromBody] OdlFaseVersamento utente)
            //{
            //    ApplicationUser user = new ApplicationUser { UserName = utente.Email, Email = utente.Email, FirstName = utente.FirstName, LastName = utente.LastName };

            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }


            //    _context.Add(utente);
            //    await _context.SaveChangesAsync();

            //    return this.Ok(utente);
            //}

            // PUT api/<OdlFaseVersamentoApiController>/5
            [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OdlFaseVersamentoApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
