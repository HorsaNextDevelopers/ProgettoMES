using AuthSystem.Areas.Identity.Data;
using AuthSystem.Areas.Identity.Pages.Account;
using AuthSystem.Models;
using AuthSystem.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AuthSystem.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly NContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

       

        public AccountController(NContext context, UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager, ILogger<RegisterModel> logger, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser {UserName = viewModel.Email, Email = viewModel.Email, FirstName = viewModel.FirstName, LastName = viewModel.LastName};
                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    var result2 = await _userManager.AddToRoleAsync(user, "User");
                    
                    _logger.LogInformation("User created a new account with password.");

                  var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(viewModel.Email, "Confirm your email",
                     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    /* if (_userManager.Options.SignIn.RequireConfirmedAccount)
                     {
                         return RedirectToPage("RegisterConfirmation", new { email = viewModel.Email, returnUrl = returnUrl });
                     }
                     else
                     {
                         await _signInManager.SignInAsync(user, isPersistent: false);
                         return LocalRedirect(returnUrl);
                     }*/
                    ViewBag.OpSuccess = true;
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(viewModel);
        }

        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = viewModel.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(viewModel);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(viewModel);
        }



    }
}
