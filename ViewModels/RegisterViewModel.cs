using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public int Year { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}