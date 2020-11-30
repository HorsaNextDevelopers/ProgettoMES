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
    public class OdlFaseVersamentoController : Controller
    {
        private readonly NContext _context;

        public OdlFaseVersamentoController(NContext context)
        {
            _context = context;
        }

        // GET: OdlFaseVersamento
        public async Task<IActionResult> Index()
        {
            var nContext = _context.OdlFaseVersamenti.Include(o => o.Fasi);
            return View(await nContext.ToListAsync());
        }

        // GET: OdlFaseVersamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odlFaseVersamento = await _context.OdlFaseVersamenti
                .Include(o => o.Fasi)
                .FirstOrDefaultAsync(m => m.IdVersamento == id);
            if (odlFaseVersamento == null)
            {
                return NotFound();
            }

            return View(odlFaseVersamento);
        }

        // GET: OdlFaseVersamento/Create
        public IActionResult Create()
        {
            ViewData["IdFase"] = new SelectList(_context.OdlFasi, "IdFaseOdl", "Fase");
            ViewData["ODl"] = new SelectList(_context.OdlFasi, "IdFaseOdl", "CodiceOdl");
            return View();
        }

        // POST: OdlFaseVersamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVersamento,Data,PezziBuoni,PezziDifettosi,TempoEffetivo,IdFase")] OdlFaseVersamento odlFaseVersamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odlFaseVersamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFase"] = new SelectList(_context.OdlFasi, "IdFaseOdl", "IdFaseOdl", odlFaseVersamento.IdFase);
            ViewData["ODl"] = new SelectList(_context.OdlFasi, "IdFaseOdl", "CodiceOdl", odlFaseVersamento.IdFase);
            return View(odlFaseVersamento);
        }

        // GET: OdlFaseVersamento/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odlFaseVersamento = await _context.OdlFaseVersamenti.FindAsync(id);
            if (odlFaseVersamento == null)
            {
                return NotFound();
            }
            ViewData["IdFase"] = new SelectList(_context.OdlFasi, "IdFaseOdl", "IdFaseOdl", odlFaseVersamento.IdFase);
            ViewData["ODl"] = new SelectList(_context.OdlFasi, "IdFaseOdl", "CodiceOdl", odlFaseVersamento.IdFase);
            return View(odlFaseVersamento);
        }

        // POST: OdlFaseVersamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVersamento,Data,PezziBuoni,PezziDifettosi,TempoEffetivo,IdFase")] OdlFaseVersamento odlFaseVersamento)
        {
            if (id != odlFaseVersamento.IdVersamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odlFaseVersamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdlFaseVersamentoExists(odlFaseVersamento.IdVersamento))
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
            ViewData["IdFase"] = new SelectList(_context.OdlFasi, "IdFaseOdl", "IdFaseOdl", odlFaseVersamento.IdFase);
            ViewData["ODl"] = new SelectList(_context.OdlFasi, "IdFaseOdl", "CodiceOdl", odlFaseVersamento.IdFase);
            return View(odlFaseVersamento);
        }

        // GET: OdlFaseVersamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odlFaseVersamento = await _context.OdlFaseVersamenti
                .Include(o => o.Fasi)
                .FirstOrDefaultAsync(m => m.IdVersamento == id);
            if (odlFaseVersamento == null)
            {
                return NotFound();
            }

            return View(odlFaseVersamento);
        }

        // POST: OdlFaseVersamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var odlFaseVersamento = await _context.OdlFaseVersamenti.FindAsync(id);
            _context.OdlFaseVersamenti.Remove(odlFaseVersamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdlFaseVersamentoExists(int id)
        {
            return _context.OdlFaseVersamenti.Any(e => e.IdVersamento == id);
        }
    }
}
