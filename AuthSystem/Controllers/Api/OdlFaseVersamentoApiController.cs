﻿using AuthSystem.Areas.Identity.Data;
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
        [HttpPost]
        [ActionName("PostControlloZero")]
        public async Task<IActionResult> PostControlloZero(int codiceOdl, int nomeFase, OdlFaseVersamento versamento)
        {

            var fase = await _context.OdlFasi
                .SingleOrDefaultAsync(m => (int)m.Fase == nomeFase && m.CodiceOdl == codiceOdl);

            var versamenti = _context.OdlFaseVersamenti.Where(m => m.Fasi.CodiceOdl == codiceOdl && (int)m.Fasi.Fase == nomeFase).ToList();

            var pezziBuoni = versamenti.Sum(s => s.PezziBuoni);

            var odl = await _context.Odls
                .SingleOrDefaultAsync(m => m.CodiceOdl == codiceOdl);

            if ((pezziBuoni + versamento.PezziBuoni) > odl.QuantitaDaProdurre)
            {
                return BadRequest("La Camilla è cattiva");

            }
            else
            {
                _context.OdlFaseVersamenti.Add(versamento);
                await _context.SaveChangesAsync();
                return Ok(versamento);
            }



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
        //[HttpPost]
        //[ActionName("PostControlloDue")]
        //public async Task<IActionResult> PostControlloDue(OdlFaseVersamento versamento)
        //{
        //    //prende un ordine di lavoro in base al codice che gli viene passato
        //    var odl = await _context.Odls
        //        .SingleOrDefaultAsync(m => m.CodiceOdl == versamento.Fasi.CodiceOdl);

        //    var fase = await _context.OdlFasi
        //        .SingleOrDefaultAsync(m => m.Fase == versamento.Fasi.Fase && m.CodiceOdl == versamento.Fasi.CodiceOdl);

        //    var versamenti = _context.OdlFaseVersamenti.Where(m => m.Fasi.CodiceOdl == versamento.Fasi.CodiceOdl && m.Fasi.Fase == (versamento.Fasi.Fase - 1)).ToList();

        //    var pezziBuoni = versamenti.Sum(s => s.PezziBuoni);
        //    return Ok(versamento);


            //controllo se la fase è F10
            //if ((int)versamento.Fasi.Fase == 1)
            //{
            //    versamenti = _context.OdlFaseVersamenti.Where(m => m.Fasi.CodiceOdl == versamento.Fasi.CodiceOdl && m.Fasi.Fase == versamento.Fasi.Fase).ToList();

            //    pezziBuoni = versamenti.Sum(s => s.PezziBuoni);

            //    //controllo se i pezzi inseriti non superano la quantià totale dell'odl
            //    if ((pezziBuoni + versamento.PezziBuoni) > odl.QuantitaDaProdurre)
            //    {
            //        return BadRequest("Cattiva Camilla, stai inserendo una quantità superiore a quella dell'odl");

            //    }
            //    else
            //    {
            //        _context.OdlFaseVersamenti.Add(versamento);
            //        await _context.SaveChangesAsync();
            //        return Ok(versamento);
            //    }

            //}
            //else
            //{
            //controllo se i pezzi inseriti in questo versamento superano i pezzi versati nella fase precedente
            //if (versamento.PezziBuoni > pezziBuoni)
            //{
            //    return BadRequest("Cattiva Camilla, stai versando una quantità di pezzi superiore al numero di pezzi disponibili dalla fase precedente");
            //}
            //else
            //{
            //    _context.OdlFaseVersamenti.Add(versamento);
            //    await _context.SaveChangesAsync();
            //    return Ok(versamento);

            //}

            //}



            //if (fase.Fase == "F10")
            //{

            //}



            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}


            //_context.Add(utente);
            //await _context.SaveChangesAsync();

            //return this.Ok(fase);
        //}

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
