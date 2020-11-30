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
    public class DistintaBaseController : Controller
    {
        private readonly NContext _context;

        public DistintaBaseController(NContext context)
        {
            _context = context;
        }

        // GET: DistintaBase
        public async Task<IActionResult> Index()
        {
            var nContext = _context.DistintaBasi.Include(d => d.ArticoloFiglio).Include(d => d.ArticoloPadre);
            return View(await nContext.ToListAsync());
        }

        // GET: DistintaBase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distintaBase = await _context.DistintaBasi
                .Include(d => d.ArticoloFiglio)
                .Include(d => d.ArticoloPadre)
                .FirstOrDefaultAsync(m => m.IdDistintaBase == id);
            if (distintaBase == null)
            {
                return NotFound();
            }

            return View(distintaBase);
        }

        // GET: DistintaBase/Create
        public IActionResult Create()
        {
            ViewData["CodiceFiglio"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo");
            ViewData["CodiceArticolo"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo");
            return View();
        }

        // POST: DistintaBase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDistintaBase,CodiceArticolo,CodiceFiglio,Quantità")] DistintaBase distintaBase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(distintaBase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodiceFiglio"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", distintaBase.CodiceFiglio);
            ViewData["CodiceArticolo"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", distintaBase.CodiceArticolo);
            return View(distintaBase);
        }

        // GET: DistintaBase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distintaBase = await _context.DistintaBasi.FindAsync(id);
            if (distintaBase == null)
            {
                return NotFound();
            }
            ViewData["CodiceFiglio"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", distintaBase.CodiceFiglio);
            ViewData["CodiceArticolo"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", distintaBase.CodiceArticolo);
            return View(distintaBase);
        }

        // POST: DistintaBase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDistintaBase,CodiceArticolo,CodiceFiglio,Quantità")] DistintaBase distintaBase)
        {
            if (id != distintaBase.IdDistintaBase)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distintaBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistintaBaseExists(distintaBase.IdDistintaBase))
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
            ViewData["CodiceFiglio"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", distintaBase.CodiceFiglio);
            ViewData["CodiceArticolo"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", distintaBase.CodiceArticolo);
            return View(distintaBase);
        }

        // GET: DistintaBase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distintaBase = await _context.DistintaBasi
                .Include(d => d.ArticoloFiglio)
                .Include(d => d.ArticoloPadre)
                .FirstOrDefaultAsync(m => m.IdDistintaBase == id);
            if (distintaBase == null)
            {
                return NotFound();
            }

            return View(distintaBase);
        }

        // POST: DistintaBase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distintaBase = await _context.DistintaBasi.FindAsync(id);
            _context.DistintaBasi.Remove(distintaBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistintaBaseExists(int id)
        {
            return _context.DistintaBasi.Any(e => e.IdDistintaBase == id);
        }
    }
}
