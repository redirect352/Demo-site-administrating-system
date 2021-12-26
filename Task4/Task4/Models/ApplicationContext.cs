using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace Task4.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
           // Database.EnsureCreated();
        }
        public DbSet<Status> Statuses { get; set; }
    }
}
