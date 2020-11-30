using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AuthSystem.Controllers.Api
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UtentiApiController : ControllerBase
    {
        private readonly NContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UtentiApiController(NContext context, UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: api/<UtentiApiController>
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            return _context.AspNetUsers;
        }

        // GET api/<UtentiApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            var utente = await _context.AspNetUsers.SingleOrDefaultAsync(m => m.Id == id);

            if (utente == null)
            {
                return NotFound();
            }
            return this.Ok(utente);
        }

        [HttpGet]
        [Route("GetNomeUtente/{nome}")]
        public IActionResult GetNomeUser(string nome)
        {
            var utente = _context.AspNetUsers
               .Where(m => m.UserName.ToLower().Contains(nome.ToLower()));
            if (utente == null)
            {
                return NotFound();
            }

            return this.Ok(utente.ToList());
        }

        // POST api/<UtentiApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationUser utente)
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

        // PUT api/<UtentiApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ApplicationUser utente)
        {
            if (id != utente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aspnetusers = _context.AspNetUsers.Single(u => u.Id.Equals(id));
                    aspnetusers.FirstName = utente.FirstName;
                    aspnetusers.LastName = utente.LastName;
                    aspnetusers.Email = utente.Email;
                    _context.Update(aspnetusers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(utente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               

            }
            return this.Ok(utente);
        }

        // DELETE api/<UtentiApiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplicationUser>> Delete(string id)
        {
            var user = await _context.AspNetUsers
              .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            _context.AspNetUsers.Remove(user);
            await _context.SaveChangesAsync();

            return this.Ok(user);
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}


