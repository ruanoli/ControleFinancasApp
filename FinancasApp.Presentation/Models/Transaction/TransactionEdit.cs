using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Transaction
{
    public class TransactionEdit
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public Guid? CategoryId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int TypeId { get; set; }

    }
}
