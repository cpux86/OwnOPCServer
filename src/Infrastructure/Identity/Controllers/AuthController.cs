using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Identity.Modeles;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModle = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };
            return View(viewModle);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) 
            {
                return View(viewModel);
            }
            //var user = await _userManager.FindByNameAsync(viewModel.UserName);
            //if (user == null)
            //{
            //    ModelState.AddModelError(string.Empty, "User not found");
            //    return View(viewModel);
            //}

            var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Login error");
           
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }
        
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = new AppUser
            {
                UserName = viewModel.UserName
            };

            
            var result = await _userManager.CreateAsync(user,viewModel.Password);
            
            if (result.Succeeded)
            {
                //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admintistrator"));

                await _userManager.AddClaimsAsync(user, 
                new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admintistrator"),
                    new Claim(ClaimTypes.Email, "cpux86@mail.ru"),
                    new Claim("test", "Admintistrator")
                }) ;
                await _signInManager.SignInAsync(user, false);
                return Register(viewModel.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Error occurred");
            return View(viewModel);

        }
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
