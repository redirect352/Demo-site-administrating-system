using Microsoft.AspNetCore.Identity;
using System;

namespace Task4.Models
{
    public class User:IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set;}
    }
}
