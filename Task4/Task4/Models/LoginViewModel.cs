﻿using System.ComponentModel.DataAnnotations;

namespace Task4.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
       
        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
