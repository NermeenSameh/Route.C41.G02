using System.ComponentModel.DataAnnotations;

namespace Route.C41.G02.PL.ViewModels
{
	public class ForgetPasswordViweModel
	{

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
