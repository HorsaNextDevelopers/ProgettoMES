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
    public class CentroDiLavoroController : Controller
    {
        private readonly NContext _context;

        public CentroDiLavoroController(NContext context)
        {
            _context = context;
        }

        // GET: CentroDiLavoro
        public async Task<IActionResult> Index()
        {
            return View(await _context.CentriDiLavoro.ToListAsync());
        }

        // GET: CentroDiLavoro/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroDiLavoro = await _context.CentriDiLavoro
                .FirstOrDefaultAsync(m => m.CodiceCentroDiLavoro == id);
            if (centroDiLavoro == null)
            {
                return NotFound();
            }

            return View(centroDiLavoro);
        }

        // GET: CentroDiLavoro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CentroDiLavoro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodiceCentroDiLavoro,Descrzione")] CentroDiLavoro centroDiLavoro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centroDiLavoro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centroDiLavoro);
        }

        // GET: CentroDiLavoro/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroDiLavoro = await _context.CentriDiLavoro.FindAsync(id);
            if (centroDiLavoro == null)
            {
                return NotFound();
            }
            return View(centroDiLavoro);
        }

        // POST: CentroDiLavoro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodiceCentroDiLavoro,Descrzione")] CentroDiLavoro centroDiLavoro)
        {
            if (id != centroDiLavoro.CodiceCentroDiLavoro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centroDiLavoro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentroDiLavoroExists(centroDiLavoro.CodiceCentroDiLavoro))
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
            return View(centroDiLavoro);
        }

        // GET: CentroDiLavoro/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroDiLavoro = await _context.CentriDiLavoro
                .FirstOrDefaultAsync(m => m.CodiceCentroDiLavoro == id);
            if (centroDiLavoro == null)
            {
                return NotFound();
            }

            return View(centroDiLavoro);
        }

        // POST: CentroDiLavoro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var centroDiLavoro = await _context.CentriDiLavoro.FindAsync(id);
            _context.CentriDiLavoro.Remove(centroDiLavoro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentroDiLavoroExists(string id)
        {
            return _context.CentriDiLavoro.Any(e => e.CodiceCentroDiLavoro == id);
        }
    }
}
