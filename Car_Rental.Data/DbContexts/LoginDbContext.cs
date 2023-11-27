using Car_Rental.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental.Data.DbContexts
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext() : base()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Car_Rental_Management_System;Trusted_Connection=True;Encrypt=Yes;TrustServerCertificate=Yes");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Login> Login { get; set; }

        public DbSet<Car> Car { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Pending_Rental> Pending_Rental { get; set; }
        public DbSet<Rentals_History> Rentals_History { get; set; }

    }
}
