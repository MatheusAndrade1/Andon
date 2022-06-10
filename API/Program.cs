using System.Net;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
