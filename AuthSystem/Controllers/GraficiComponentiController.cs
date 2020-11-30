using System;
using System.Linq;
using AuthSystem.Models;
using AuthSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthSystem.Controllers
{
    [Authorize]
    public class GraficiComponentiController : Controller
    {

        private readonly NContext _context;

        public GraficiComponentiController(NContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var articoli = _context.Articoli.ToList();

            var viewModel = new GraficoComponentiViewModel();

            viewModel.Articoli = new SelectList(articoli, "IdArticolo", "NomeArticolo");

            return View(viewModel);
        }

    }
}
