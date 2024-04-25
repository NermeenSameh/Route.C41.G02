using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.Extensions;
using Route.C41.G02.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.G02.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);


			#region Configure Services


			webApplicationBuilder.Services.AddControllersWithViews();  // Register Built-In Services Required by Mvc

			#region Comment
			/// webApplicationBuilder.Services.AddMvc();
			/// webApplicationBuilder.Services.AddRazorPages();

			/// webApplicationBuilder.Services.AddTransient<ApplicationDbContext>();
			/// webApplicationBuilder.Services.AddSingleton<ApplicationDbContext>();
			///  webApplicationBuilder.Services.AddScoped<ApplicationDbContext>();

			/// webApplicationBuilder.Services.AddScoped<ApplicationDbContext>();
			/// webApplicationBuilder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>();

			//  webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>();

			/// webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(
			///     contextLifetime: ServiceLifetime.Singleton,
			///     optionsLifetime: ServiceLifetime.Singleton
			///     
			///     );

			/// webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
			/// {
			///     options.UseSqlServer("Server = .; Database = MvcApplicationG02; Trusted_Connection  = True; MultipleActiveResultSets = False");
			/// }); 
			#endregion

			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});


			// ApplicationServicesExtientions.AddApplicationServices(webApplicationBuilder.Services); // Static Method

			webApplicationBuilder.Services.AddApplicationServices();  // Extension Method

			webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

			/// webApplicationBuilder.Services.AddScoped<UserManager<ApplicationUser>>();
			/// webApplicationBuilder.Services.AddScoped<SignInManager<ApplicationUser>>();
			/// webApplicationBuilder.Services.AddScoped<RoleManager<IdentityRole>>();


			webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
				option.Password.RequiredUniqueChars = 2;
				option.Password.RequireDigit = true;
				option.Password.RequireNonAlphanumeric = true;
				option.Password.RequireUppercase = true;
				option.Password.RequireLowercase = true;
				option.Password.RequiredLength = 5;

				option.Lockout.AllowedForNewUsers = true;
				option.Lockout.MaxFailedAccessAttempts = 5;
				option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);


			})
				.AddEntityFrameworkStores<ApplicationDbContext>();

			webApplicationBuilder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.ExpireTimeSpan = TimeSpan.FromDays(5);
				options.AccessDeniedPath = "/Home/Error";
			});

			webApplicationBuilder.Services.AddAuthentication(options =>
			{
				// options.DefaultAuthenticateScheme = "Hamda";

			})
				.AddCookie("Cookies", options =>
				{
					options.LoginPath = "/Account/SignIn";
					options.ExpireTimeSpan = TimeSpan.FromDays(5);
					options.AccessDeniedPath = "/Home/Error";
				});

			#endregion

			var app = webApplicationBuilder.Build();


			#region Configure Kestral Middleware

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});


			#endregion

			app.Run(); // Application is Ready for Request
		}


	}
}
