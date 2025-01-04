using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Domain.Models;
using FinancasApp.Presentation.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FinancasApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region Cria o acesso do usuário e grava o arquivo de cookie no navegador.
                    var user = _userService.Authenticate(viewModel.Email, viewModel.Password);

                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };

                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(user, settings))
                    },
                    CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    #endregion

                    return RedirectToAction("Dashboard", "Home");

                }
                catch (Exception ex)
                {

                    TempData["MessageErro"] = ex.Message;

                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User()
                    {
                        Name = viewModel.Name,
                        Email = viewModel.Email,
                        Password = viewModel.Password
                    };

                    _userService.CreateAccount(user);

                    TempData["MessageSuccess"] = "Conta criada com sucesso!";
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception ex)
                {

                    TempData["MessageErro"] = ex.Message;
                }
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(AccountForgotPasswordViewModel viewModel)
        {
            return View();
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
