using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Models;
using FinancasApp.Domain.Models.Enums;
using FinancasApp.Presentation.Models.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FinancasApp.Presentation.Controllers
{
    [Authorize]
    public class TransactonController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactonController(ICategoryRepository categoryRepository, ITransactionRepository transactionRepository)
        {
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository;
        }

        public IActionResult Consult()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }


        public IActionResult Register()
        {
            ViewBag.Categories = GetCategories();
            ViewBag.TransactionTypes = Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();

            return View(new TransactionRegister());

        }

        [HttpPost]
        public IActionResult Register(TransactionRegister viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioAuth = JsonConvert.DeserializeObject<User>(User.Identity.Name);
                    var transaction = new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        Name = viewModel.Name,
                        CategoryId = viewModel.CategoryId ?? Guid.Empty,
                        Description = viewModel.Description,
                        DataTransaction = viewModel.Date == null ? default(DateTime) : viewModel.Date.Value,
                        Type = (TransactionType)viewModel.TypeId,
                        UserId = usuarioAuth.Id,
                        Value = viewModel.Value
                    };

                    ModelState.Clear();
                    _transactionRepository.Add(transaction);
                    TempData["MessageSuccess"] = $"Conta {transaction.Name} cadastrada com Sucesso!";

                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = ex.Message;
                }
            }

            ViewBag.Categories = GetCategories();
            ViewBag.TransactionTypes = Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();

            return View(viewModel);
        }


        private List<SelectListItem> GetCategories()
        {
            var listCategories = new List<SelectListItem>();

            try
            {
                var userAuth = JsonConvert.DeserializeObject<User>(User.Identity.Name);

                var categries = _categoryRepository.GetCategoryByUser(userAuth.Id);

                foreach (var category in categries)
                {
                    listCategories.Add(
                        new SelectListItem
                        {
                            Value = category.Id.ToString(),
                            Text = category.Name
                        });
                }

            }
            catch (Exception ex)
            {

                TempData["MessageError"] = ex.Message;
            }

            return listCategories;
        }
    }
}
