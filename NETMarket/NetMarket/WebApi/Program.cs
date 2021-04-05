using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Instancia del host que va a ejecutar la aplicación que es el WebApi:
            var host = CreateHostBuilder(args).Build();

            //Creación del using var scope:
            using (var scope = host.Services.CreateScope())
            {
                //Provider que va a permitir ejecutar la migración y instanciar el DbContext:
                var services = scope.ServiceProvider;

                //Instanciar el LoggerFactory para imprimir los errores que puedan ocurrir durante las migraciones:
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    //Invocación del DBContext:
                    var context = services.GetRequiredService<MarketDbContext>();

                    //Ejecución de la migración:
                    await context.Database.MigrateAsync();

                    //Ejecutar la carga de los datos de los archivos Json:
                    await MarketDbContextData.CargarDataAsync(context, loggerFactory);
                }
                catch (Exception ex)
                {
                    //Imprimir los errores en caso de que existiera errores:
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "Errores en el proceso de migración");
                }
            }

            //Ejecución del Host:
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
