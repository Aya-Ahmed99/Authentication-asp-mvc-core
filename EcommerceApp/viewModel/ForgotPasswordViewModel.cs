using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.viewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
