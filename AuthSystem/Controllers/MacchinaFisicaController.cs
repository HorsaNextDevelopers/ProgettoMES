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
    public class MacchinaFisicaController : Controller
    {
        private readonly NContext _context;

        public MacchinaFisicaController(NContext context)
        {
            _context = context;
        }

        // GET: MacchinaFisica
        public async Task<IActionResult> Index()
        {
            var nContext = _context.MacchinaFisica.Include(m => m.CentriDiLavoro);
            return View(await nContext.ToListAsync());
        }

        // GET: MacchinaFisica/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var macchinaFisica = await _context.MacchinaFisica
                .Include(m => m.CentriDiLavoro)
                .FirstOrDefaultAsync(m => m.CodiceMacchinaFisica == id);
            if (macchinaFisica == null)
            {
                return NotFound();
            }

            return View(macchinaFisica);
        }

        // GET: MacchinaFisica/Create
        public IActionResult Create()
        {
            ViewData["CodiceCentroDiLavoro"] = new SelectList(_context.CentriDiLavoro, "CodiceCentroDiLavoro", "CodiceCentroDiLavoro");
            return View();
        }

        // POST: MacchinaFisica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodiceMacchinaFisica,Descrizione,CodiceCentroDiLavoro")] MacchinaFisica macchinaFisica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(macchinaFisica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodiceCentroDiLavoro"] = new SelectList(_context.CentriDiLavoro, "CodiceCentroDiLavoro", "CodiceCentroDiLavoro", macchinaFisica.CodiceCentroDiLavoro);
            return View(macchinaFisica);
        }

        // GET: MacchinaFisica/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var macchinaFisica = await _context.MacchinaFisica.FindAsync(id);
            if (macchinaFisica == null)
            {
                return NotFound();
            }
            ViewData["CodiceCentroDiLavoro"] = new SelectList(_context.CentriDiLavoro, "CodiceCentroDiLavoro", "CodiceCentroDiLavoro", macchinaFisica.CodiceCentroDiLavoro);
            return View(macchinaFisica);
        }

        // POST: MacchinaFisica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodiceMacchinaFisica,Descrizione,CodiceCentroDiLavoro")] MacchinaFisica macchinaFisica)
        {
            if (id != macchinaFisica.CodiceMacchinaFisica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(macchinaFisica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MacchinaFisicaExists(macchinaFisica.CodiceMacchinaFisica))
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
            ViewData["CodiceCentroDiLavoro"] = new SelectList(_context.CentriDiLavoro, "CodiceCentroDiLavoro", "CodiceCentroDiLavoro", macchinaFisica.CodiceCentroDiLavoro);
            return View(macchinaFisica);
        }

        // GET: MacchinaFisica/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var macchinaFisica = await _context.MacchinaFisica
                .Include(m => m.CentriDiLavoro)
                .FirstOrDefaultAsync(m => m.CodiceMacchinaFisica == id);
            if (macchinaFisica == null)
            {
                return NotFound();
            }

            return View(macchinaFisica);
        }

        // POST: MacchinaFisica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var macchinaFisica = await _context.MacchinaFisica.FindAsync(id);
            _context.MacchinaFisica.Remove(macchinaFisica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MacchinaFisicaExists(string id)
        {
            return _context.MacchinaFisica.Any(e => e.CodiceMacchinaFisica == id);
        }
    }
}
