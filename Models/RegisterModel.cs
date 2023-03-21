using System.ComponentModel.DataAnnotations;
using WebRunApplication.Enums;

namespace WebRunApplication.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [Display(Name = nameof(Login))]
        [MinLength(8, ErrorMessage = "Логин должен иметь длину не менее 8 символов")]
        [MaxLength(50, ErrorMessage = "Логин должен иметь длину не более 50 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password))]
        [MinLength(8, ErrorMessage = "Пароль должен иметь длину не менее 8 символов")]
        [MaxLength(50, ErrorMessage = "Пароль должен иметь длину не более 50 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Необходимо подтвердить пароль")]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Необходимо ввести адрес почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Необходимо ввести имя")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Необходимо указать возраст")]
        public uint Age { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать пол")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Необходимо ввести вес")]
        public uint Weight { get; set; }

        [Required(ErrorMessage = "Необходимо ввести рост")]
        public uint Height { get; set; }
    }
}
