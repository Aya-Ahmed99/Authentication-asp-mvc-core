using EcommerceApp.viewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Models
{
    public class ecommerceContext : IdentityDbContext<ApplicationUser>
    {
        public ecommerceContext() : base()
        {
        }
        public ecommerceContext(DbContextOptions options ) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
            base.OnConfiguring(optionsBuilder);
        }

        protected async override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

 
        }
        public DbSet<Order> Orders { get; set; }

    }
}
