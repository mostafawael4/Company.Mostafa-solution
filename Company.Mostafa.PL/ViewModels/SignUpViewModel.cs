using System.ComponentModel.DataAnnotations;

namespace Company.Mostafa.PL.ViewModels
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage ="UserName is required")]
        public string UserName { get; set; }
		[Required(ErrorMessage = "FirstName is required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "LastName is required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		[MinLength(5,ErrorMessage ="Password too short")]
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfirmPassword is required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "IsAgree is required")]
		public bool IsAgree { get; set; }
    }
}

