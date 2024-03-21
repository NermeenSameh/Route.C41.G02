using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.G02.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
          var hostBuiler =  CreateHostBuilder(args).Build();
           // Data Seeding      
           // Apply Migrations 

            hostBuiler.Run(); // Application is Ready For Requests
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
