using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.viewModel
{
    public class LoginVM
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememebrMe { get; set; }
    }
}
