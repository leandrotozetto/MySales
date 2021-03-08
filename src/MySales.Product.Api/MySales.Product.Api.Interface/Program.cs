using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MySales.Product.Api.Interface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //BuildWebHost(args).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //         .ConfigureAppConfiguration((hostingContext, configuration) =>
        //         {
        //             configuration.SetBasePath(Directory.GetCurrentDirectory())
        //             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //             .AddEnvironmentVariables().Build();
        //         })
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.ConfigureKestrel(serverOptions => { })
        //            .UseStartup<Startup>();
        //        });



        //public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //     .UseKestrel()
        //     .UseContentRoot(Directory.GetCurrentDirectory())
        //     .UseIISIntegration()
        //     //.ConfigureLogging(l => l.AddConsole(config.GetSection("Logging")))
        //     .ConfigureAppConfiguration((hostingContext, configuration) =>
        //     {
        //         configuration.SetBasePath(Directory.GetCurrentDirectory())
        //         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //         .AddEnvironmentVariables().Build();
        //     })
        //     .UseStartup<Startup>()
        //     .Build();
    }
}
