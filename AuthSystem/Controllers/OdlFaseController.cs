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
    public class OdlFaseController : Controller
    {
        private readonly NContext _context;

        public OdlFaseController(NContext context)
        {
            _context = context;
        }

        // GET: OdlFase
        public async Task<IActionResult> Index()
        {
            var nContext = _context.OdlFasi.Include(o => o.MacchineFisiche).Include(o => o.Odls);
            return View(await nContext.ToListAsync());
        }

        // GET: OdlFase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odlFase = await _context.OdlFasi
                .Include(o => o.MacchineFisiche)
                .Include(o => o.Odls)
                .FirstOrDefaultAsync(m => m.IdFaseOdl == id);
            if (odlFase == null)
            {
                return NotFound();
            }

            return View(odlFase);
        }

        // GET: OdlFase/Create
        public IActionResult Create()
        {
            ViewData["CodiceMacchinaFisica"] = new SelectList(_context.MacchinaFisica, "CodiceMacchinaFisica", "CodiceMacchinaFisica");
            ViewData["CodiceOdl"] = new SelectList(_context.Odls, "CodiceOdl", "CodiceOdl");
            return View();
        }

        // POST: OdlFase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFaseOdl,CodiceOdl,Fase,Descrizione,CodiceMacchinaFisica,TempoStandard,Stato")] OdlFase odlFase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odlFase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodiceMacchinaFisica"] = new SelectList(_context.MacchinaFisica, "CodiceMacchinaFisica", "CodiceMacchinaFisica", odlFase.CodiceMacchinaFisica);
            ViewData["CodiceOdl"] = new SelectList(_context.Odls, "CodiceOdl", "CodiceOdl", odlFase.CodiceOdl);
            return View(odlFase);
        }

        // GET: OdlFase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odlFase = await _context.OdlFasi.FindAsync(id);
            if (odlFase == null)
            {
                return NotFound();
            }
            ViewData["CodiceMacchinaFisica"] = new SelectList(_context.MacchinaFisica, "CodiceMacchinaFisica", "CodiceMacchinaFisica", odlFase.CodiceMacchinaFisica);
            ViewData["CodiceOdl"] = new SelectList(_context.Odls, "CodiceOdl", "CodiceOdl", odlFase.CodiceOdl);
            return View(odlFase);
        }

        // POST: OdlFase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFaseOdl,CodiceOdl,Fase,Descrizione,CodiceMacchinaFisica,TempoStandard,Stato")] OdlFase odlFase)
        {
            if (id != odlFase.IdFaseOdl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odlFase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdlFaseExists(odlFase.IdFaseOdl))
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
            ViewData["CodiceMacchinaFisica"] = new SelectList(_context.MacchinaFisica, "CodiceMacchinaFisica", "CodiceMacchinaFisica", odlFase.CodiceMacchinaFisica);
            ViewData["CodiceOdl"] = new SelectList(_context.Odls, "CodiceOdl", "CodiceOdl", odlFase.CodiceOdl);
            return View(odlFase);
        }

        // GET: OdlFase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odlFase = await _context.OdlFasi
                .Include(o => o.MacchineFisiche)
                .Include(o => o.Odls)
                .FirstOrDefaultAsync(m => m.IdFaseOdl == id);
            if (odlFase == null)
            {
                return NotFound();
            }

            return View(odlFase);
        }

        // POST: OdlFase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odlFase = await _context.OdlFasi.FindAsync(id);
            _context.OdlFasi.Remove(odlFase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdlFaseExists(int id)
        {
            return _context.OdlFasi.Any(e => e.IdFaseOdl == id);
        }
    }
}
