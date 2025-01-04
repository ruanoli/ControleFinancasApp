using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Models;
using FinancasApp.Presentation.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinancasApp.Presentation.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Consult()
        {
            var user = JsonConvert.DeserializeObject<User>(User.Identity.Name);
            var categories = _categoryRepository.GetCategoryByUser(user.Id);
            var categoryViewModel = new List<CategoryConsult>();


            foreach (var item in categories)
            {
                categoryViewModel.Add(
                        new CategoryConsult()
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
            }

            return View(categoryViewModel);
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(CategoryRegister viewModel)
        {
            if (ModelState.IsValid)
            {
                var userAuthenticate = JsonConvert.DeserializeObject<User>(User.Identity.Name);

                try
                {
                    var category = new Category();
                    category.Id = Guid.NewGuid();
                    category.Name = viewModel.Name;
                    category.UserId = userAuthenticate.Id;

                    _categoryRepository.Add(category);

                    ModelState.Clear();
                    //return RedirectToAction("Consult", "Category");
                    TempData["MessageSuccess"] = $"Categoria {category.Name} cadastrada com sucesso!";

                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = ex.Message;
                }

            }

            return View();
        }

        public IActionResult Delete(Guid idCategory)
        {
            try
            {
                var category = _categoryRepository.GetById(idCategory);

                if (category != null)
                {
                    _categoryRepository.Delete(category);
                }

            }
            catch (Exception ex)
            {
                TempData["MessageError"] = ex.Message;
            }

            return RedirectToAction("Consult", "Category");
        }

        public IActionResult Edit(Guid idCategory)
        {
            var category = _categoryRepository.GetById(idCategory);
            var categoryViewModel = new CategoryEdit();
            if (category != null)
            {
                categoryViewModel.Name = category.Name;
                categoryViewModel.UserId = category.UserId;
                categoryViewModel.Id = category.Id;
            }
            return View(categoryViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryEdit viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = _categoryRepository.GetById(viewModel.Id);

                    if (category != null)
                    {
                        category.Name = viewModel.Name;
                        _categoryRepository.Update(category);
                        
                        return RedirectToAction("Consult", "Category");
                    }
                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = ex.Message;
                }
            }

            return View();
        }

    }

}
