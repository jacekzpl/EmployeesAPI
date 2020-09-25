using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Owin.Hosting;
using System.Web.Http;


namespace EmployeesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string baseUri = "http://localhost:5000";
            //Console.WriteLine("Starting web Server...");
            //WebApp.Start<Startup>(baseUri);
            //Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
