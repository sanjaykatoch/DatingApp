using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
             var host=CreateHostBuilder(args).Build();
             using var scope=host.Services.CreateScope();
             var service=scope.ServiceProvider;
             try{
                 var context=service.GetRequiredService<DataContext>();
                 await context.Database.MigrateAsync();//by using this we don't need to run migration command
                 await Seed.SeedUsers(context);
             }
             catch(Exception ex){
                 //this is used for catch the error while adding data
                 var logger=service.GetRequiredService<ILogger<Program>>();
                 logger.LogError(ex,"An error ocuured during migration");
             }
             await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
