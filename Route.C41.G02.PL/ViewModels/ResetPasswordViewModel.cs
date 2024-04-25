using System.ComponentModel.DataAnnotations;

namespace Route.C41.G02.PL.ViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "New Password is required")]
        [MinLength(5, ErrorMessage = "Minmum Password Lenght is 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't match with Password")]
        public string ConfirmPassword { get; set; }

    }
}
