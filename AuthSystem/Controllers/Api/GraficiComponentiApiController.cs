/*using System;
using System.Linq;
using AuthSystem.Models;
using AuthSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthSystem.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GraficiComponentiApiController : ControllerBase
    {

        private readonly NContext _context;

        public GraficiComponentiApiController(NContext context)
        {
            _context = context;
        }


        // GET api/<GraficiComponentiApiController>/5
        [HttpGet]
        [Route("GetComponenti/{idArticolo}")]
        public IActionResult GetComponenti(string codiceArticolo)
        {
            var componenti = _context.ComponentiArticolo.Where(c => c.codiceArticolo == CodiceArticolo).ToList();

            return this.Ok(componenti);
        }

        [HttpGet]
        [Route("GetDatiGrafico/{idComponente}")]
        public IActionResult GetDatiGrafico(int idComponente)
        {
            var viewModel = new ChartComponentiModel();

            var versamenti = _context.Versamenti.Where(c => c.IdComponente == idComponente).ToList();

            var componente = _context.ComponentiArticolo.Single(c => c.IdComponente == idComponente);

            viewModel.Labels = versamenti.Select(s => s.Data.ToShortDateString()).ToArray();

            viewModel.Dataset = versamenti.Select(s => s.PezziBuoni == 0 ? 0 : Convert.ToInt32(s.TempoProd / (s.PezziBuoni + s.PezziDifettosi))).ToArray();

            viewModel.TempoIdeale = Convert.ToInt32(componente.TempoProduzione);

            return this.Ok(viewModel);
        }
    }
}
*/