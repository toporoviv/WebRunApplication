using System.ComponentModel.DataAnnotations;
using WebRunApplication.DataEntity;

namespace WebRunApplication.Models
{
    public class AuthorizationModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [Display(Name = "Login")]
        [MinLength(8, ErrorMessage = "Логин должен иметь длину не менее 8 символов")]
        [MaxLength(50, ErrorMessage = "Логин должен иметь длину не более 50 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину не менее 6 символов")]
        [MaxLength(50, ErrorMessage = "Пароль должен иметь длину не более 50 символов")]
        public string Password { get; set; }
    }
}
