using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Models;


namespace EcommerceApp.Models
{
    public class ecommerceContext : IdentityDbContext<ApplicationUser>
    {
        public ecommerceContext()
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
            builder.Entity<IdentityRole>().HasData(
                new { Id = "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", Name = "Admin" },
                new { Id = "f2c3f0d1-77e5-4fc2-bb59-b6b811380be6", Name = "Customer" }
                );

            ApplicationUser user = new ApplicationUser()
            {
                FullName = "Admin",
                Email = "ayaahmed199910@gmail.com",
                Address = "cairo",
                BirthDate = "28-10-1999",
                Gender = "Female",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "AYAAHMED199910@GMAIL.COM",
                EmailConfirmed = true
            };
            var password = "Admin1234#";
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;
            builder.Entity<ApplicationUser>().HasData(user);

            builder.Entity<IdentityUserRole<string>>()
                     .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = user.Id, RoleId = "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7" });
            

            base.OnModelCreating(builder);
        }
        public DbSet<Order> Orders { get; set; }

    }
}
