using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.WebUI.ViewModels
{
    public class RegisterViewDTO
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Repita a Senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senha é diferente.")]
        public string ConfirmPassword { get; set; }
    }
}
