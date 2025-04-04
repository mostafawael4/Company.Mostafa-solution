using System.ComponentModel.DataAnnotations;

namespace Company.Mostafa.PL.ViewModels.Account
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }
	}
}

