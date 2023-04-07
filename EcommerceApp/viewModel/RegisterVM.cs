using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.viewModel
{
    public class RegisterVM
    {
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^[A-Za-z]+([\s][A-Za-z]+)*$", ErrorMessage = "Enter Vaild Name")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }
    }
}
