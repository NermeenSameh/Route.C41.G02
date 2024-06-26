﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.Services.EmailSender;
using Route.C41.G02.PL.ViewModels;
using System.Threading.Tasks;

namespace Route.C41.G02.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
			IEmailSender emailSender,
			IConfiguration configuration,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_emailSender = emailSender;
			_configuration = configuration;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region Sign Up

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]

		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.UserName);

				if (user is null)
				{
					user = new ApplicationUser()
					{
						FName = model.FName,
						LName = model.LName,
						UserName = model.UserName,
						Email = model.Email,
						IsAgree = model.IsAgree,
					};

					var Result = await _userManager.CreateAsync(user, model.Password);

					if (Result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
				ModelState.AddModelError(string.Empty, "This UserName is already in use for another account!");
			}
			return View(model);
		}



		#endregion

		#region Sign In


		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

						if (result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Account is Locked");

						if (result.IsNotAllowed)
							ModelState.AddModelError(string.Empty, "Your Account is not Confirmed yet!");

						if (result.Succeeded)
							return RedirectToAction(nameof(HomeController.Index), "Home");

					}
				}

				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(model);
		}
		#endregion

		#region Sign Out
		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction(nameof(SignIn));

		}

		#endregion

		#region Forget Password

		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViweModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
					 
					var resetPasswordUrl = Url.Action("ResertPassword" , "Account" , new {email = user.Email , token = resetPasswordToken });

					await _emailSender.SendAsync(
					from: _configuration["EmailSetting:SenderEmail"],
					recipients: model.Email,
					subject: "Reset Your Password",
					body: resetPasswordUrl);

					return RedirectToAction(nameof(CheckYourIndex));
				}
				ModelState.AddModelError(string.Empty, "There is No Account with this Email !");
			}

			return View(model);
		}


		public IActionResult CheckYourIndex()
		{
			return View();
		}

		#endregion


		#region Reset Password
		[HttpGet]
		public IActionResult ResetPassword(string email , string token)
		{
			TempData["Email"] = email;
			TempData["Token"] = token;
			return View();
		}

		[HttpPost]
		public async Task< IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if(ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["Token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				
				if(user is not  null)
				{

				await _userManager.ResetPasswordAsync(user , token , model.NewPassword);
					return RedirectToAction(nameof(SignIn));
				}
				ModelState.AddModelError(string.Empty, "Url is not vaild");
			}

			return View(model);
		}
		

		#endregion
	}
}
