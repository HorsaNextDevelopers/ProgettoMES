using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Models;

namespace AuthSystem.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdlFaseApiController : ControllerBase
    {
        private readonly NContext _context;

        public OdlFaseApiController(NContext context)
        {
            _context = context;
        }

        // GET: api/OdlFaseApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OdlFase>>> GetOdlFasi()
        {
            return await _context.OdlFasi.ToListAsync();
        }

        // GET: api/OdlFaseApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OdlFase>> GetOdlFase(int id)
        {
            var odlFase = await _context.OdlFasi.FindAsync(id);

            if (odlFase == null)
            {
                return NotFound();
            }

            return odlFase;
        }

        // PUT: api/OdlFaseApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOdlFase(int id, OdlFase odlFase)
        {
            if (id != odlFase.IdFaseOdl)
            {
                return BadRequest();
            }

            _context.Entry(odlFase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OdlFaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OdlFaseApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OdlFase>> PostOdlFase(OdlFase odlFase)
        {
            _context.OdlFasi.Add(odlFase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOdlFase", new { id = odlFase.IdFaseOdl }, odlFase);
        }

        // DELETE: api/OdlFaseApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OdlFase>> DeleteOdlFase(int id)
        {
            var odlFase = await _context.OdlFasi.FindAsync(id);
            if (odlFase == null)
            {
                return NotFound();
            }

            _context.OdlFasi.Remove(odlFase);
            await _context.SaveChangesAsync();

            return odlFase;
        }

        private bool OdlFaseExists(int id)
        {
            return _context.OdlFasi.Any(e => e.IdFaseOdl == id);
        }
    }
}
