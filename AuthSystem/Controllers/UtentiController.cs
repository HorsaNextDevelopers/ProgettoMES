using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Controllers
{
   
    public class UtentiController : Controller
    {
      
        private readonly NContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UtentiController(NContext context, UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var utenti = _context.AspNetUsers.ToList();
            // var utenti2 = _userManager.Users.ToList();
            return View(utenti);
        }

        /*  public async IActionResult Create()
          {
               ApplicationUser user = new ApplicationUser { UserName = utente.Email, Email = utente.Email, FirstName = utente.FirstName, LastName = utente.LastName };
               var result = await _userManager.Users.First().FirstName.CreateAsync(user, "pwd");
              return View();


             // var u = _userManager.Users.First();
            //  u.FirstName = "aaa";
            //  _userManager.UpdateAsync(u);
             // _userManager.ResetAuthenticatorKeyAsync(u);
            //  return null;
          }*/
        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser utente)
        {
            ApplicationUser user = new ApplicationUser { UserName = utente.Email, Email = utente.Email, FirstName = utente.FirstName, LastName = utente.LastName };
            if (ModelState.IsValid)
            {
                _context.Add(utente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utente);
        }

        // GET: Articolo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.AspNetUsers.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            return View(utente);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser utente)
        {
            
            if (id != utente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   var aspnetusers =  _context.AspNetUsers.Single(u => u.Id.Equals(id));
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
                return RedirectToAction(nameof(Index));
           
            }
            return View(utente);
        }

        

        // GET: Articolo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // POST: Articolo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var utente = await _context.AspNetUsers.FindAsync(id);
            _context.AspNetUsers.Remove(utente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }



    }
}

