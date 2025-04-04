using System.ComponentModel.DataAnnotations;

namespace Company.Mostafa.PL.ViewModels
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Password too short")]
		public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

