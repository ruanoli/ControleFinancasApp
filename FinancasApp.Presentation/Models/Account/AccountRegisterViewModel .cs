using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Account
{
    public class AccountRegisterViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage = "Sua senha deve ter: pelo menos uma letra maiúscula, pelo menos uma letra minúscula, pelo menos um número e pelo menos um caractere especial.")]
        [Required(ErrorMessage = "Informe a senha")]
        public string? Password { get; set; }


        [Required(ErrorMessage = "Confirme sua senha")]
        [Compare("Password", ErrorMessage = "Senhas diferentes")]
        public string? CornfirmedPassword { get; set; }
    }
}
