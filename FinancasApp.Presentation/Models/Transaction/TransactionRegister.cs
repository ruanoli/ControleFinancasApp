using FinancasApp.Domain.Models;
using FinancasApp.Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Transaction
{
    public class TransactionRegister
    {
        public TransactionRegister() { }

        //public TransactionRegister(List<SelectListItem> selectListItemCategory)
        //{
        //    TransactionTypes = Enum.GetValues(typeof(TransactionType))
        //            .Cast<TransactionType>()
        //            .Select(e => new SelectListItem
        //            {
        //                Value = ((int)e).ToString(),
        //                Text = e.ToString()
        //            }).ToList(); 
            
        //    Categories = selectListItemCategory;
        //}

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public Guid? CategoryId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Valor inválido.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int TypeId { get; set; }

        //public List<SelectListItem> TransactionTypes { get; set; }
        //public List<SelectListItem> Categories { get; set; }


    }
}
