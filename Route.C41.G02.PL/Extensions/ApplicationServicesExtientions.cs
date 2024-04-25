using Microsoft.Extensions.DependencyInjection;
using Route.C41.G01.BLL;
using Route.C41.G01.BLL.Interfaces;
using Route.C41.G01.BLL.Repositories;
using Route.C41.G02.PL.Services.EmailSender;

namespace Route.C41.G02.PL.Extensions
{
    public static class ApplicationServicesExtientions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
           // services.AddScoped<IDepartmentRepository, DepartmentRepository>();
           //
           // services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            /// services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            /// services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
       
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IUniteOfWork ,UniteOfWork>();
        

            return services;
        }
    }
}
