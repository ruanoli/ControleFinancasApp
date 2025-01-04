using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Category
{
    public class CategoryEdit
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        public string? Name { get; set; }
        public Guid UserId { get; set; }
    }
}
