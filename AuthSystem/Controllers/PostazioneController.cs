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
    public class PostazioneController : Controller
    {
        private readonly NContext _context;

        public PostazioneController(NContext context)
        {
            _context = context;
        }

        // GET: Postazione
        public async Task<IActionResult> Index()
        {
            return View(await _context.Postazioni.ToListAsync());
        }

        // GET: Postazione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postazione = await _context.Postazioni
                .FirstOrDefaultAsync(m => m.IdPostazione == id);
            if (postazione == null)
            {
                return NotFound();
            }

            return View(postazione);
        }

        // GET: Postazione/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Postazione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPostazione,NomePostazione,Descrizione")] Postazione postazione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postazione);
        }

        // GET: Postazione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postazione = await _context.Postazioni.FindAsync(id);
            if (postazione == null)
            {
                return NotFound();
            }
            return View(postazione);
        }

        // POST: Postazione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPostazione,NomePostazione,Descrizione")] Postazione postazione)
        {
            if (id != postazione.IdPostazione)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostazioneExists(postazione.IdPostazione))
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
            return View(postazione);
        }

        // GET: Postazione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postazione = await _context.Postazioni
                .FirstOrDefaultAsync(m => m.IdPostazione == id);
            if (postazione == null)
            {
                return NotFound();
            }

            return View(postazione);
        }

        // POST: Postazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postazione = await _context.Postazioni.FindAsync(id);
            _context.Postazioni.Remove(postazione);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostazioneExists(int id)
        {
            return _context.Postazioni.Any(e => e.IdPostazione == id);
        }
    }
}
