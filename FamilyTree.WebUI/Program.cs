using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Privacy.Interfaces;
using FamilyTree.Infrastructure.Persistence;
using FamilyTree.WebUI.Hubs;
using FamilyTree.WebUI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.WebUI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                try
                {
                    ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }                
            }            

            //Weird algorithm to check privacy end date
            Timer timer = new Timer(PrivacyTimerCallback, host, 0, 5000);

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async void PrivacyTimerCallback(object host)
        {
            using (IServiceScope scope = ((IHost)host).Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                IPrivacyNotificationsService notificationsService = services.GetRequiredService<IPrivacyNotificationsService>();

                await notificationsService.NotifyUsersIfPrivacyTimeExpired();
            }
        }
    }
}
