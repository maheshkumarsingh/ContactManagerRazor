using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using Repositories;
using Serilog;
using ContactsManager.Filters.ActionFilters;
using ContactsManager.StartUpExtension;
namespace ContactsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Serilog
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration) =>
            {
                configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
            });
            //Logging
            //builder.Host.ConfigureLogging(loggingProvider =>
            //{
            //    loggingProvider.ClearProviders();
            //    loggingProvider.AddConsole();
            //    loggingProvider.AddDebug();
            //    loggingProvider.AddEventLog();
            //});
            builder.Services.ConfigureService();
                        
            WebApplication app = builder.Build();

            app.UseSerilogRequestLogging();

            if (builder.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();
            //HTTP logging
            app.UseHttpLogging();
            
            Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot",wkhtmltopdfRelativePath: "Rotativa");
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
