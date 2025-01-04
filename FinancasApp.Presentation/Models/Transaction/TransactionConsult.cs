using FinancasApp.Domain.Models;
using FinancasApp.Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Transaction
{
    public class TransactionConsult
    {

        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime? InitialDate { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime? FinalDate { get; set; }
    }
}
