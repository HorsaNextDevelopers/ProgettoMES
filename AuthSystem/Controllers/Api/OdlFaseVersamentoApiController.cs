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

        // POST api/<OdlFaseVersamentoApiController>
        [HttpPost]
        [ActionName("PostControlloUno")]
        public async Task<IActionResult> Post([FromBody] OdlFaseVersamento utente)
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
        [ActionName("PostControlloDue")]
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
        }
    }
}
