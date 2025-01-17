using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Models;
using FinancasApp.Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinancasApp.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public HomeController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public JsonResult GetTransactions(DateTime initDate, DateTime finalDate)
        {
            try
            {
                var userAtuth = JsonConvert.DeserializeObject<User>(User.Identity.Name);
                var transactions = _transactionRepository.GetTransactionByDateAndUser(initDate, finalDate, userAtuth.Id);

                //Agrupa as contas pelo tipo e faz o somatório.
                var totalForTypes = transactions.GroupBy(x => x.Type)
                    .Select(c => new
                    {
                        Type = c.Key.ToString(),
                        Total = c.Sum(c => c.Value)
                    }).ToList();

                var totalForCategories = transactions.Where(x => x.Type == TransactionType.Despesa)
                    .GroupBy(x => x.Category)
                    .Select(c => new
                    {
                        CategoryName = c.Key.Name,
                        Total = c.Sum(x => x.Value)
                    }).ToList();

                return Json(new {totalForTypes, totalForCategories });

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }
    }
}
