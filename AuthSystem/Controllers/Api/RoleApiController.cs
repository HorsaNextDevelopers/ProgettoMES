using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthSystem.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleApiController : ControllerBase
    {
        private readonly NContext _context;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleApiController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg, NContext context)
        {
            roleManager = roleMgr;
            userManager = userMrg;
            _context = context;
        }


        // GET: api/<RoleApiController>
        [HttpGet]
        public IEnumerable<IdentityRole> Get()
        {
            return roleManager.Roles;
        }

        // GET api/<RoleApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoleApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoleApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoleApiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IdentityRole>> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
              
                ModelState.AddModelError("", "No role found");
                return NotFound();
               
            }
            IdentityResult result = await roleManager.DeleteAsync(role);

            return this.Ok(roleManager.Roles);
        }
       
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
