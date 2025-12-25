using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "نام کاربری الزامی است.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = true;
    }
}