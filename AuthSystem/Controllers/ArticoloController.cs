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
    public class ArticoloController : Controller
    {
        private readonly NContext _context;

        public ArticoloController(NContext context)
        {
            _context = context;
        }

        // GET: Articolo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articoli.ToListAsync());
        }

        // GET: Articolo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articoli
                .FirstOrDefaultAsync(m => m.CodiceArticolo == id);
            if (articolo == null)
            {
                return NotFound();
            }

            return View(articolo);
        }

        // GET: Articolo/Create
        public IActionResult Create()
        {
            var categoria = new List<SelectListItem>()
            {
                new SelectListItem() {Text= CategoriaArtEnum.ProdottoFinito.ToString(), Value= ((int)CategoriaArtEnum.ProdottoFinito).ToString()},
                new SelectListItem() {Text= CategoriaArtEnum.Semilavorato.ToString(), Value= ((int)CategoriaArtEnum.Semilavorato).ToString()},
                new SelectListItem() {Text= CategoriaArtEnum.MateriaPrima.ToString(), Value= ((int)CategoriaArtEnum.MateriaPrima).ToString()},

            };

            ViewData["Categoria"] = categoria;
            return View();
        }

        // POST: Articolo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodiceArticolo,Descrizione,Categoria")] Articolo articolo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articolo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categoria = new List<SelectListItem>()
            {
                new SelectListItem() {Text= CategoriaArtEnum.ProdottoFinito.ToString(), Value= ((int)CategoriaArtEnum.ProdottoFinito).ToString()},
                new SelectListItem() {Text= CategoriaArtEnum.Semilavorato.ToString(), Value= ((int)CategoriaArtEnum.Semilavorato).ToString()},
                new SelectListItem() {Text= CategoriaArtEnum.MateriaPrima.ToString(), Value= ((int)CategoriaArtEnum.MateriaPrima).ToString()},

            };

            ViewData["Categoria"] = categoria;
            return View(articolo);
        }

        // GET: Articolo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articoli.FindAsync(id);
            if (articolo == null)
            {
                return NotFound();
            }
            return View(articolo);
        }

        // POST: Articolo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodiceArticolo,Descrizione,Categoria")] Articolo articolo)
        {
            if (id != articolo.CodiceArticolo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articolo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticoloExists(articolo.CodiceArticolo))
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
            return View(articolo);
        }

        // GET: Articolo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articolo = await _context.Articoli
                .FirstOrDefaultAsync(m => m.CodiceArticolo == id);
            if (articolo == null)
            {
                return NotFound();
            }

            return View(articolo);
        }

        // POST: Articolo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var articolo = await _context.Articoli.FindAsync(id);
            _context.Articoli.Remove(articolo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticoloExists(string id)
        {
            return _context.Articoli.Any(e => e.CodiceArticolo == id);
        }
    }
}
