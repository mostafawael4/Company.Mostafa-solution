using System.ComponentModel.DataAnnotations;

namespace Company.Mostafa.PL.ViewModels.Account
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Password too short")]
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfirmPassword is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}

