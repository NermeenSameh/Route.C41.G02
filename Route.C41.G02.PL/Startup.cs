using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.BLL.Repositories;
using Route.C41.G02.DAL.Data;
using Route.C41.G02.DAL.Models;
using Route.C41.G02.PL.Extensions;
using Route.C41.G02.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.G02.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();  // Register Built-In Services Required by Mvc

            /// services.AddMvc();
            /// services.AddRazorPages();

            /// services.AddTransient<ApplicationDbContext>();
            /// services.AddSingleton<ApplicationDbContext>();
            ///  services.AddScoped<ApplicationDbContext>();

            /// services.AddScoped<ApplicationDbContext>();
            /// services.AddScoped<DbContextOptions<ApplicationDbContext>>();

            //  services.AddDbContext<ApplicationDbContext>();

            /// services.AddDbContext<ApplicationDbContext>(
            ///     contextLifetime: ServiceLifetime.Singleton,
            ///     optionsLifetime: ServiceLifetime.Singleton
            ///     
            ///     );

            /// services.AddDbContext<ApplicationDbContext>(options =>
            /// {
            ///     options.UseSqlServer("Server = .; Database = MvcApplicationG02; Trusted_Connection  = True; MultipleActiveResultSets = False");
            /// });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            
            // ApplicationServicesExtientions.AddApplicationServices(services); // Static Method

             services.AddApplicationServices();  // Extension Method

            services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            /// services.AddScoped<UserManager<ApplicationUser>>();
            /// services.AddScoped<SignInManager<ApplicationUser>>();
            /// services.AddScoped<RoleManager<IdentityRole>>();


            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
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

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.AccessDeniedPath = "/Home/Error";
            });

            services.AddAuthentication(options =>
            {
                // options.DefaultAuthenticateScheme = "Hamda";
                
            })
                .AddCookie("Cookies" , options =>
                {
					options.LoginPath = "/Account/SignIn";
					options.ExpireTimeSpan = TimeSpan.FromDays(5);
					options.AccessDeniedPath = "/Home/Error";
				});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
        }
    }
}
