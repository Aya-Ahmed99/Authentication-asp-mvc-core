using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public string FullName { get; set; }

        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
