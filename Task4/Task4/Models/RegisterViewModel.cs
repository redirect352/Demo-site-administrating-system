using System.ComponentModel.DataAnnotations;

namespace Task4.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [Required]
        [Compare("Password",ErrorMessage ="Passwords arent equal")]
        [Display(Name = " Password Confirm")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
