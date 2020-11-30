using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Models;

namespace AuthSystem.Controllers
{
    public class PrenotazioneController : Controller
    {
        private readonly NContext _context;

        public PrenotazioneController(NContext context)
        {
            _context = context;
        }

        // GET: Prenotazione
        public async Task<IActionResult> Index()
        {
            var nContext = _context.Prenotazioni.Include(p => p.AspNetUsers).Include(p => p.Postazioni);
            return View(await nContext.ToListAsync());
        }

        // GET: Prenotazione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.AspNetUsers)
                .Include(p => p.Postazioni)
                .FirstOrDefaultAsync(m => m.IdPrenotazione == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // GET: Prenotazione/Create
        public IActionResult Create()
        {
            ViewData["IdAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Email");
            ViewData["IdPostazione"] = new SelectList(_context.Postazioni, "IdPostazione", "NomePostazione");
            return View();
        }

        // POST: Prenotazione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrenotazione,Data,IdAspNetUsers,IdPostazione")] Prenotazione prenotazione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prenotazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Email", prenotazione.IdAspNetUsers);
            ViewData["IdPostazione"] = new SelectList(_context.Postazioni, "IdPostazione", "NomePostazione", prenotazione.IdPostazione);
            return View(prenotazione);
        }

        // GET: Prenotazione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            if (prenotazione == null)
            {
                return NotFound();
            }
            ViewData["IdAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Email", prenotazione.IdAspNetUsers);
            ViewData["IdPostazione"] = new SelectList(_context.Postazioni, "IdPostazione", "NomePostazione", prenotazione.IdPostazione);
            return View(prenotazione);
        }

        // POST: Prenotazione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrenotazione,Data,IdAspNetUsers,IdPostazione")] Prenotazione prenotazione)
        {
            if (id != prenotazione.IdPrenotazione)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prenotazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrenotazioneExists(prenotazione.IdPrenotazione))
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
            ViewData["IdAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Email", prenotazione.IdAspNetUsers);
            ViewData["IdPostazione"] = new SelectList(_context.Postazioni, "IdPostazione", "NomePostazione", prenotazione.IdPostazione);
            return View(prenotazione);
        }

        // GET: Prenotazione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prenotazione = await _context.Prenotazioni
                .Include(p => p.AspNetUsers)
                .Include(p => p.Postazioni)
                .FirstOrDefaultAsync(m => m.IdPrenotazione == id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            return View(prenotazione);
        }

        // POST: Prenotazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);
            _context.Prenotazioni.Remove(prenotazione);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrenotazioneExists(int id)
        {
            return _context.Prenotazioni.Any(e => e.IdPrenotazione == id);
        }
    }
}
