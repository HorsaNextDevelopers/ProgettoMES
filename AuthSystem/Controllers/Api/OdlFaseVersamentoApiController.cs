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



     
        //[HttpPost]
        //[ActionName("AvviaFase")]
        //public async Task<IActionResult> AvviaFase(OdlFaseVersamento odlFaseVersamento, int idFase)
        //{
        //    var fase = _context.OdlFasi.SingleOrDefault(p => p.IdFaseOdl == idFase);
        //    if (fase == null)
        //    {
        //        return Ok(new ApiResult<OdlFaseVersamento>()
        //        {
        //            Ok = false,
        //            Message = "La fase non è stata trovata!"
        //        });
        //    }
        //    //avvio della fase
        //    var odl = _context.Odls.SingleOrDefault(p => p.CodiceOdl == fase.CodiceOdl);
        //    if (odl.Stato == OdlStateEnum.Nuovo && fase.Stato == OdlStateEnum.Nuovo && fase.Fase == FaseODLEnum.F10)
        //    {
        //        //cambio lo stato della fase
        //        fase.Stato = OdlStateEnum.InCorso;
        //        //cambio lo stato dell'odl
        //        odl.Stato = OdlStateEnum.InCorso;
        //        return Ok(new ApiResult<OdlFaseVersamento>()
        //        {
        //            Ok = false,
        //            Message = "La fase F10 è in corso!"
        //        });

        //    }
        //    //controllo la fase e se si è effettuato almeno un versamento con i numero dei pezzi buoni > 0 //stessa cosa f30 e f40 
        //    if (fase.Fase == FaseODLEnum.F10 && odl.Stato == OdlStateEnum.InCorso)
        //    {
        //        var fase10 = _context.OdlFasi.SingleOrDefault(p => p.CodiceOdl == odl.CodiceOdl && p.Fase == FaseODLEnum.F10);
        //        var versamentiF10 = _context.OdlFaseVersamenti.Where(p => p.IdFaseOdl == fase10.IdFaseOdl);
        //        if (versamentiF10.Any() && versamentiF10.Sum(p => p.PezziBuoni) > 0)
        //        {
        //            //fase20 può partire
        //            fase.Fase = FaseODLEnum.F20;
        //            fase.Stato = OdlStateEnum.InCorso;
        //            return Ok(new ApiResult<OdlFaseVersamento>()
        //            {
        //                Ok = false,
        //                Message = "La fase F20 è in corso!"
        //            });
        //        }
        //    }
        //    //se non passo per nessuna delle if bad request
        //    if (!((odl.Stato == OdlStateEnum.Nuovo && fase.Stato == OdlStateEnum.Nuovo && fase.Fase == FaseODLEnum.F10) && (fase.Fase == FaseODLEnum.F10 && odl.Stato == OdlStateEnum.InCorso)))
        //    {
        //        return BadRequest();
        //    }

        //    _context.OdlFaseVersamenti.Add(odlFaseVersamento);
        //    await _context.SaveChangesAsync();

        //    return Ok(new ApiResult<OdlFaseVersamento>()
        //    {
        //        Ok = true,
        //        DataResult = odlFaseVersamento = new OdlFaseVersamento
        //        {
        //            Data = odlFaseVersamento.Data,
        //            PezziBuoni = odlFaseVersamento.PezziBuoni,
        //            PezziDifettosi = odlFaseVersamento.PezziDifettosi,
        //            IdFaseOdl = idFase,
        //            TempoEffetivo = odlFaseVersamento.TempoEffetivo,
        //            IdVersamento = 0
        //        }
        //    });
        //}


        //POST api/<OdlFaseVersamento1ApiController>
        [HttpPost]
        [ActionName("PostControlloDue")]
        public async Task<IActionResult> PostControlloDue(int codiceOdl, int nomeFase, int stato, OdlFaseVersamento versamento)
        {
            //prende un ordine di lavoro in base al codice che gli viene passato
            var odl = await _context.Odls
                .SingleOrDefaultAsync(m => m.CodiceOdl == codiceOdl && (int)m.Stato == stato);

            var fase = await _context.OdlFasi
                .SingleOrDefaultAsync(m => (int)m.Fase == nomeFase && m.CodiceOdl == codiceOdl && (int)m.Stato == stato);

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
                    return BadRequest("Cattiva Camilla, stai inserendo una quantità superiore a quella dell'odl");

                }
                else
                {
                    _context.OdlFaseVersamenti.Add(versamento);
                    await _context.SaveChangesAsync();
                    var pezziBuoniFaseAttuale2 = versamentiFaseAttuale.Sum(s => s.PezziBuoni);
                    pezziBuoniFaseAttuale2 = pezziBuoniFaseAttuale2 + versamento.PezziBuoni;
                    if (versamentiFaseAttuale.Count() == 1)
                    {
                        //cambio lo stato della fase
                        fase.Stato = OdlStateEnum.InCorso;
                        //cambio lo stato dell'odl
                        odl.Stato = OdlStateEnum.InCorso;
                    }
                    if (odl.QuantitaDaProdurre == pezziBuoniFaseAttuale2)
                    {
                        //cambio lo stato della fase in completato
                        fase.Stato = OdlStateEnum.Completato;
                    }
                    return Ok(versamento);
                }

            }
            else   //se la fase è diversa dalla fase 10
            {

                //controllo se esistono versamenti nella fase precedente alla fase attuale
                if(pezziBuoniFasePrecedente == 0)
                {

                    return BadRequest("Cattiva Camilla, non puoi fare versamenti in questa fase poichè non esistono versamenti precedenti");
                }
                //controllo se i pezzi inseriti in questo versamento superano i pezzi versati nella fase precedente
                if (versamento.PezziBuoni > pezziBuoniFasePrecedente - pezziBuoniFaseAttuale)
                {
                    return BadRequest("Cattiva Camilla, stai versando una quantità di pezzi superiore al numero di pezzi disponibili dalla fase precedente");
                }
                else
                {
                    _context.OdlFaseVersamenti.Add(versamento);
                    await _context.SaveChangesAsync();
                    //cambio stato della fase attuale
                    if (versamentiFaseAttuale.Count() == 1)
                    {
                        //cambio lo stato della fase
                        fase.Stato = OdlStateEnum.InCorso;
    
                    }
                    //controllo se il versamento attuale va a completare l'odl
                    var fasiOdl = _context.OdlFasi.Where(m => m.CodiceOdl == codiceOdl ).ToList();
                    var conteggioFasi = fasiOdl.Count();
                    var pezziBuoniFaseAttuale2 = versamentiFaseAttuale.Sum(s => s.PezziBuoni);
                    pezziBuoniFaseAttuale2 = pezziBuoniFaseAttuale2 + versamento.PezziBuoni;
                    if (odl.QuantitaDaProdurre == pezziBuoniFaseAttuale2)
                    {
                        //cambio lo stato della fase
                        fase.Stato = OdlStateEnum.Completato;
                    }
                    if (nomeFase == conteggioFasi && odl.QuantitaDaProdurre == pezziBuoniFaseAttuale2)
                    {
                        // cambio stato odl in completato
                        odl.Stato = OdlStateEnum.Completato;

                        return this.Ok("Hai completato l'odl: " + odl.CodiceOdl);
                    }
                    return Ok(versamento);

                }

            }
        }   

    }
}
