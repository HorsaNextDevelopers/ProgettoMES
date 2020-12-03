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
    public class OdlController : Controller
    {
        private readonly NContext _context;

        public OdlController(NContext context)
        {
            _context = context;
        }

        // GET: Odl
        public async Task<IActionResult> Index()
        {
            var nContext = _context.Odls.Include(o => o.Articoli).Include(o => o.CentriDiLavoro);
            return View(await nContext.ToListAsync());
        }

        // GET: Odl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odl = await _context.Odls
                .Include(o => o.Articoli)
                .Include(o => o.CentriDiLavoro)
                .FirstOrDefaultAsync(m => m.CodiceOdl == id);
            if (odl == null)
            {
                return NotFound();
            }

            return View(odl);
        }

        // GET: Odl/Create
        public IActionResult Create()
        {
            ViewData["CodiceArticolo"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo");
            ViewData["CodiceCentroDiLavoro"] = new SelectList(_context.CentriDiLavoro, "CodiceCentroDiLavoro", "CodiceCentroDiLavoro");
            var stato = new List<SelectListItem>()
            {
                new SelectListItem() {Text= OdlStateEnum.Nuovo.ToString(), Value= ((int)OdlStateEnum.Nuovo).ToString()},
                new SelectListItem() {Text= OdlStateEnum.Completato.ToString(), Value= ((int)OdlStateEnum.Completato).ToString()},
                new SelectListItem() {Text= OdlStateEnum.InCorso.ToString(), Value= ((int)OdlStateEnum.InCorso).ToString()}
               
            };

            ViewData["Stato"] = stato;
            return View();
        }

        // POST: Odl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodiceOdl,QuantitaDaProdurre,DataInizio,DataFine,CodiceArticolo,CodiceCentroDiLavoro,Stato")] Odl odl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodiceArticolo"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", odl.CodiceArticolo);
            ViewData["CodiceCentroDiLavoro"] = new SelectList(_context.CentriDiLavoro, "CodiceCentroDiLavoro", "CodiceCentroDiLavoro", odl.CodiceCentroDiLavoro);
            var stato = new List<SelectListItem>()
            {
                new SelectListItem() {Text= OdlStateEnum.Nuovo.ToString(), Value= ((int)OdlStateEnum.Nuovo).ToString()},
                new SelectListItem() {Text= OdlStateEnum.Completato.ToString(), Value= ((int)OdlStateEnum.Completato).ToString()},
                new SelectListItem() {Text= OdlStateEnum.InCorso.ToString(), Value= ((int)OdlStateEnum.InCorso).ToString()}
               
            };

            ViewData["Stato"] = stato;
            return View(odl);
        }

        // GET: Odl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odl = await _context.Odls.FindAsync(id);
            if (odl == null)
            {
                return NotFound();
            }
            ViewData["CodiceArticolo"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", odl.CodiceArticolo);
            ViewData["CodiceCentroDiLavoro"] = new SelectList(_context.CentriDiLavoro, "CodiceCentroDiLavoro", "CodiceCentroDiLavoro", odl.CodiceCentroDiLavoro);
            var stato = new List<SelectListItem>()
            {
                new SelectListItem() {Text= OdlStateEnum.Nuovo.ToString(), Value= ((int)OdlStateEnum.Nuovo).ToString()},
                new SelectListItem() {Text= OdlStateEnum.Completato.ToString(), Value= ((int)OdlStateEnum.Completato).ToString()},
                new SelectListItem() {Text= OdlStateEnum.InCorso.ToString(), Value= ((int)OdlStateEnum.InCorso).ToString()}
              
            };

            ViewData["Stato"] = stato;
            return View(odl);
        }

        // POST: Odl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodiceOdl,QuantitaDaProdurre,DataInizio,DataFine,CodiceArticolo,CodiceCentroDiLavoro,Stato")] Odl odl)
        {
            if (id != odl.CodiceOdl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdlExists(odl.CodiceOdl))
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
            ViewData["CodiceArticolo"] = new SelectList(_context.Articoli, "CodiceArticolo", "CodiceArticolo", odl.CodiceArticolo);
            ViewData["CodiceCentroDiLavoro"] = new SelectList(_context.CentriDiLavoro, "CodiceCentroDiLavoro", "CodiceCentroDiLavoro", odl.CodiceCentroDiLavoro);
            var stato = new List<SelectListItem>()
            {
                new SelectListItem() {Text= OdlStateEnum.Nuovo.ToString(), Value= ((int)OdlStateEnum.Nuovo).ToString()},
                new SelectListItem() {Text= OdlStateEnum.Completato.ToString(), Value= ((int)OdlStateEnum.Completato).ToString()},
                new SelectListItem() {Text= OdlStateEnum.InCorso.ToString(), Value= ((int)OdlStateEnum.InCorso).ToString()}
               
            };

            ViewData["Stato"] = stato;
            return View(odl);
        }

        // GET: Odl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odl = await _context.Odls
                .Include(o => o.Articoli)
                .Include(o => o.CentriDiLavoro)
                .FirstOrDefaultAsync(m => m.CodiceOdl == id);
            if (odl == null)
            {
                return NotFound();
            }

            return View(odl);
        }

        // POST: Odl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odl = await _context.Odls.FindAsync(id);
            _context.Odls.Remove(odl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdlExists(int id)
        {
            return _context.Odls.Any(e => e.CodiceOdl == id);
        }
    }
}
