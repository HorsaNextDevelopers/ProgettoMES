using AuthSystem.ApiModel;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        [HttpGet]
        [Route("GetPezziBuoni/{pezziBuoni}")]
        public async Task<IActionResult> GetPezziBuoni(int pezziBuoni)
        {
            var pzB = await _context.OdlFaseVersamenti
                .FirstOrDefaultAsync(m => m.PezziBuoni == pezziBuoni);
            return this.Ok(pzB);
        }
        
        [HttpGet]
        [Route("GetFase/{fase}")]
        public IActionResult GetFase(int fase)
        {
            var fs =  _context.OdlFaseVersamenti
                .Where(m => m.IdFaseOdl == fase);
            return this.Ok(fs);
        }
       
        //inserimento versamento
        //sospensione versamento

        /*[HttpPost]
        [ActionName("InserisciVersamento")]
        public IActionResult InserisciVersamento([FromBody] OdlFaseVersamento odlFaseVersamento)
        {
            //pezziBuoni < della qtà da produrre
            //pezziBuoni > 0
            //la fase deve essere attiva idfase = fase.inCorso
            //contatore per i diversi versamenti di una fase
            //quando il cotatore = quantità da produrre allora fase.completata
            //bool fase completata
            //se la fase attuale > fase10 recupero i versamenti precedenti 
            return this.Ok(faseAttuale);
        }*/


        // POST api/<OdlFaseVersamentoApiController>
        [HttpPost]
        [ActionName("AvviaFase")]
        public async Task<IActionResult> AvviaFase(OdlFaseVersamento odlFaseVersamento ,int idFase)
        {
            var fase = _context.OdlFasi.SingleOrDefault(p => p.IdFaseOdl == idFase);
            if(fase == null)
            {
                return Ok(new ApiResult<OdlFaseVersamento>()
                {
                    Ok = false,
                    Message = "La fase non è stata trovata!"
                });
            }
            //avvio della fase
            var odl = _context.Odls.SingleOrDefault(p => p.CodiceOdl == fase.CodiceOdl);
            if(odl.Stato == OdlStateEnum.Nuovo && fase.Stato == OdlStateEnum.Nuovo && fase.Fase == FaseODLEnum.F10)
            {
                //cambio lo stato della fase
                fase.Stato = OdlStateEnum.InCorso;
                //cambio lo stato dell'odl
                odl.Stato = OdlStateEnum.InCorso;
                return Ok(new ApiResult<OdlFaseVersamento>()
                {
                    Ok = false,
                    Message = "La fase F10 è in corso!"
                });

            }
            //controllo la fase e se si è effettuato almeno un versamento con i numero dei pezzi buoni > 0 //stessa cosa f30 e f40 
            //controllo la fase e se si è effettuato almeno un versamento con i numero dei pezzi buoni > 0
            if (fase.Fase == FaseODLEnum.F20 && odl.Stato == OdlStateEnum.InCorso)
            {
                var fase20 = _context.OdlFasi.SingleOrDefault(p => p.CodiceOdl == odl.CodiceOdl && p.Fase == FaseODLEnum.F20);
                var versamentiF20 = _context.OdlFaseVersamenti.Where(p => p.IdFaseOdl == fase20.IdFaseOdl);
                if (versamentiF20.Any() && versamentiF20.Sum(p => p.PezziBuoni) > 0)
                {
                    //fase30 può partire
                    fase.Fase = FaseODLEnum.F30;
                    fase.Stato = OdlStateEnum.InCorso;
                    return Ok(new ApiResult<OdlFaseVersamento>()
                    {
                        Ok = false,
                        Message = "La fase F230 è in corso!"
                    });
                }
            }
            //controllo la fase e se si è effettuato almeno un versamento con i numero dei pezzi buoni > 0
            if (fase.Fase == FaseODLEnum.F30 && odl.Stato == OdlStateEnum.InCorso)
            {
                var fase30 = _context.OdlFasi.SingleOrDefault(p => p.CodiceOdl == odl.CodiceOdl && p.Fase == FaseODLEnum.F30);
                var versamentiF30 = _context.OdlFaseVersamenti.Where(p => p.IdFaseOdl == fase30.IdFaseOdl);
                if (versamentiF30.Any() && versamentiF30.Sum(p => p.PezziBuoni) > 0)
                {
                    //fase40 può partire
                    fase.Fase = FaseODLEnum.F40;
                    fase.Stato = OdlStateEnum.InCorso;
                    return Ok(new ApiResult<OdlFaseVersamento>()
                    {
                        Ok = false,
                        Message = "La fase F40 è in corso!"
                    });
                }
            }
            //ordine completato se f40 è concluso e f40 è conclusa quando i suoi pezzi buoni sono uguali alla quantità da produrre dell'odl
            if(fase.Fase == FaseODLEnum.F40 && odl.Stato == OdlStateEnum.InCorso)
            {
                var fase40 = _context.OdlFasi.SingleOrDefault(p => p.CodiceOdl == odl.CodiceOdl && p.Fase == FaseODLEnum.F40);
                var versamentiF40 = _context.OdlFaseVersamenti.Where(p => p.IdFaseOdl == fase40.IdFaseOdl);
                if (versamentiF40.Any() && versamentiF40.Sum(p => p.PezziBuoni) == odl.QuantitaDaProdurre)
                {
                    //fase40 completata
                    fase.Stato = OdlStateEnum.Completato;
                    odl.Stato = OdlStateEnum.Completato;
                    return Ok(new ApiResult<OdlFaseVersamento>()
                    {
                        Ok = false,
                        Message = "L'ordine di lavoro è completato!"
                    });
                }
            }
            //se non passo per nessuna delle if bad request
            if ((fase == null) && (odl.Stato == OdlStateEnum.Nuovo && fase.Stato == OdlStateEnum.Nuovo && fase.Fase == FaseODLEnum.F10) && (fase.Fase == FaseODLEnum.F10 && odl.Stato == OdlStateEnum.InCorso
                ) && (fase.Fase == FaseODLEnum.F20 && odl.Stato == OdlStateEnum.InCorso) && (fase.Fase == FaseODLEnum.F30 && odl.Stato == OdlStateEnum.InCorso) && (fase.Fase == FaseODLEnum.F40 && odl.Stato == OdlStateEnum.InCorso))
            {
                return BadRequest();
            }
            
            _context.OdlFaseVersamenti.Add(odlFaseVersamento);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<OdlFaseVersamento>()
            {
                Ok = true,
                DataResult = odlFaseVersamento = new OdlFaseVersamento
                {
                    Data = odlFaseVersamento.Data,
                    PezziBuoni = odlFaseVersamento.PezziBuoni,
                    PezziDifettosi = odlFaseVersamento.PezziDifettosi,
                    IdFaseOdl = idFase,
                    TempoEffetivo = odlFaseVersamento.TempoEffetivo,
                    IdVersamento = 0
                }
            });
        }

            /* [HttpPost]
             [ActionName("PostControlloDue") ]
             public async Task<IActionResult> PosPostControlloDuet([FromBody] OdlFaseVersamento utente)
             {
                 ApplicationUser user = new ApplicationUser { UserName = utente.Email, Email = utente.Email, FirstName = utente.FirstName, LastName = utente.LastName };

                 if (!ModelState.IsValid)
                 {
                     return BadRequest(ModelState);
                 }


                 _context.Add(utente);
                 await _context.SaveChangesAsync();

                 return this.Ok(utente);
             }

             [HttpPost]
             [ActionName("PostControlloTre")]
             public async Task<IActionResult> PostControlloTre([FromBody] OdlFaseVersamento utente)
             {
                 ApplicationUser user = new ApplicationUser { UserName = utente.Email, Email = utente.Email, FirstName = utente.FirstName, LastName = utente.LastName };

                 if (!ModelState.IsValid)
                 {
                     return BadRequest(ModelState);
                 }


                 _context.Add(utente);
                 await _context.SaveChangesAsync();

                 return this.Ok(utente);
             }

             // PUT api/<OdlFaseVersamentoApiController>/5
             [HttpPut("{id}")]
             public void Put(int id, [FromBody] string value)
             {
             }

             // DELETE api/<OdlFaseVersamentoApiController>/5
             [HttpDelete("{id}")]
             public void Delete(int id)
             {
             }*/
        }
}
