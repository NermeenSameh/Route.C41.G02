using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels;
using System.Threading.Tasks;

namespace Route.C41.G02.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
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

		#endregion
	}
}
