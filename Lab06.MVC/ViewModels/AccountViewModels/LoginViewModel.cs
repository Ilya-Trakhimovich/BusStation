using System.ComponentModel.DataAnnotations;

namespace Lab06.MVC.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(6, ErrorMessage = "Password must have at least 6 characters")]
        public string Password { get; set; }
    }
}