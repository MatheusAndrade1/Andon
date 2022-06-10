using System.Net;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
            await SeedRoles.Seed(roleManager);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    HostConfig.CertPath = context.Configuration["CertPath"]; 
                    HostConfig.CertPassword = context.Configuration["CertPassword"];
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var host = Dns.GetHostEntry("webapi.io");

                    webBuilder.ConfigureKestrel(opt =>
                    {
                        // opt.ListenAnyIP(5000);
                        opt.Listen(host.AddressList[0], 5000);
                        opt.Listen(host.AddressList[0], 5001, listOpt => {
                            listOpt.UseHttps(HostConfig.CertPath, HostConfig.CertPassword); //Enforcing the use of our certificate
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }


    // 
    public static class HostConfig
    {
        public static string CertPath {get; set;}
        public static string CertPassword {get; set;}
    }

}
