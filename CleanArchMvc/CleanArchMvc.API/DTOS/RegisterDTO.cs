using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.API.DTOS
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "E-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório!")]
        [StringLength(20, ErrorMessage = "A senha deve ter no mínimo {0} e no máximo {1} caracteres.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
