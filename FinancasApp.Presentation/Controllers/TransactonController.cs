using Bogus.DataSets;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Models;
using FinancasApp.Domain.Models.Enums;
using FinancasApp.Presentation.Models.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Globalization;

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
            var transactionViewModel = new TransactionConsult();

            var currentDate = DateTime.Now;
            var firstDay = new DateTime(currentDate.Year, currentDate.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            transactionViewModel.InitialDate = firstDay;
            transactionViewModel.FinalDate = lastDay;

            transactionViewModel = GetTransactions(transactionViewModel);


            return View(transactionViewModel);
        }

        [HttpPost]
        public IActionResult Consult(TransactionConsult viewModel)
        {

            if (ModelState.IsValid)
            {
                viewModel = GetTransactions(viewModel);
            }

            return View(viewModel);
        }

        private TransactionConsult GetTransactions(TransactionConsult viewModel)
        {
            try
            {
                var userAuth = JsonConvert.DeserializeObject<User>(User.Identity.Name);
                var transaction = _transactionRepository.GetTransactionByDateAndUser(viewModel.InitialDate.Value, viewModel.FinalDate.Value, userAuth.Id);
                viewModel.ResultConsults = new List<TransactionResultConsult>();

                if (transaction.Count > 0)
                {
                    foreach (var item in transaction)
                    {
                        viewModel.ResultConsults.Add(
                            new TransactionResultConsult()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Category = item.Category.Name,
                                Data = item.DataTransaction,
                                Type = item.Type.ToString(),
                                Value = item.Value.ToString()

                            }
                         );
                    }
                }
                else
                {
                    TempData["MessageAlert"] = "Não há contas registradas nesse período.";

                }
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = ex.Message;
            }

            return viewModel;
        }

        public IActionResult Edit(Guid idTransaction)
        {
            var transaction = _transactionRepository.GetById(idTransaction);
            var transactionViewModel = new TransactionEdit();

            transactionViewModel.Id = transaction.Id;
            transactionViewModel.Name = transaction.Name;
            transactionViewModel.Value = transaction.Value.ToString("c");
            transactionViewModel.Date = transaction.DataTransaction;
            transactionViewModel.Description = transaction.Description;
            transactionViewModel.CategoryId = transaction.CategoryId;
            transactionViewModel.TypeId = (int)transaction.Type;

            ViewBag.Categories = GetCategories();
            ViewBag.TransactionTypes = Enum.GetValues(typeof(TransactionType))
                .Cast<TransactionType>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();

            return View(transactionViewModel);
        }

        [HttpPost]
        public IActionResult Edit(TransactionEdit viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var transaction = _transactionRepository.GetById(viewModel.Id);

                    if (transaction != null)
                    {
                        transaction.Value = viewModel.Value == null ? default(decimal) : decimal.Parse(viewModel.Value, CultureInfo.InvariantCulture);
                        transaction.CategoryId = viewModel.CategoryId.Value;
                        transaction.DataTransaction = viewModel.Date.Value;
                        transaction.Description = viewModel.Description;
                        transaction.Name = viewModel.Name;
                        transaction.Type = (TransactionType)viewModel.TypeId;

                        _transactionRepository.Update(transaction);

                        TempData["MessageSuccess"] = $"Conta {transaction.Name} editada com Sucesso!";

                    }
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

            return View();
        }

        public IActionResult Delete(Guid idTransaction)
        {
            try
            {
                var transaction = _transactionRepository.GetById(idTransaction);

                if (transaction != null)
                    _transactionRepository.Delete(transaction);

                TempData["MessageSuccess"] = $"Conta {transaction.Name} excluída com Sucesso!";
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = ex.Message;
            }
            return RedirectToAction("Consult", "Transacton");
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
            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors)
            //                                   .Select(e => e.ErrorMessage)
            //                                   .ToList();
            //    foreach (var error in errors)
            //    {
            //        // Log erros para depuração              
            //    }
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var userAuth = JsonConvert.DeserializeObject<User>(User.Identity.Name);
                    var transaction = new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        Name = viewModel.Name,
                        CategoryId = viewModel.CategoryId ?? Guid.Empty,
                        Description = viewModel.Description,
                        DataTransaction = viewModel.Date == null ? default(DateTime) : viewModel.Date.Value,
                        Type = (TransactionType)viewModel.TypeId,
                        UserId = userAuth.Id,
                        Value = viewModel.Value == null ? 0 : decimal.Parse(viewModel.Value, CultureInfo.InvariantCulture)
                    };

                    _transactionRepository.Add(transaction);
                    ModelState.Clear();
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
