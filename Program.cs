using EmpreendedorismoEIT.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //Desativa migrations
            //Cria banco de dados automaticamente
            //Popula com dados em DbInitializer
            //Apagar DB manualmente quando fizer altera??es no modelo
            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    //Cria o banco de dados se ele n?o existir ou n?o tiver as tabelas necess?rias
                    if (context.Database.EnsureCreated())
                    {
                        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                        //Preenche banco com dados de teste
                        DbInitializer.Initialize(context, logger);
                        DbInitializer.AddDefaultUserAsync(userManager, roleManager, logger).GetAwaiter().GetResult();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "[DEBUG] Erro ao verificar/criar banco de dados");
                    Environment.Exit(1);
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
