﻿using System.ComponentModel.DataAnnotations;

namespace Route.C41.G02.PL.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name  is required")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last Name  is required")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Minmum Password Lenght is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password doesn't match with Password")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
