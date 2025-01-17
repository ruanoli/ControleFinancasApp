using Bogus;
using FinancasApp.Domain.Helpers;
using FinancasApp.Domain.Interfaces.Repositories;
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
        private readonly IUserRepository _userRepository;

        public AccountController(IUserService userService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userRepository.GetUserByEmail(viewModel.Email);

                    if (user != null)
                    {
                        Faker faker = new Faker();
                        var password = $"@{faker.Internet.Password(8)}{new Random().Next(999)}";

                        var emailMessageModel = new EmailMessage()
                        {
                            EmailRecipient = viewModel.Email,
                            Subject = "Recuperação de senha do Sistema de Gerenciamento de Contas.",
                            Body = $"Nova senha gerada: {password}"
                        };

                        EmailMessageHelper.Send(emailMessageModel);

                        user.Password = Sha1Helper.ComputeSHA1Hash(password);

                        _userRepository.Update(user);
                        TempData["MessageSuccess"] = $"Senha enviada para o email {viewModel.Email} com sucesso!";

                    }
                    else
                    {
                        TempData["MessageErro"] = "E-mail não encontado. Por favor, verifique se o e-mail digitado está correto.";

                    }
                }
                catch (Exception ex)
                {

                    TempData["MessageErro"] = ex.Message;
                }
            }
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
