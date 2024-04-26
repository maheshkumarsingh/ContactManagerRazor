using ContactsManager.Filters.ActionFilters;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

namespace ContactsManager.StartUpExtension
{
    public static class ConfigureServiceExtension
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            var logger = services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();
            services.AddControllersWithViews(options =>
            {
                //options.Filters.Add<ResponseHeaderActionFilter>();
                options.Filters.Add(new ResponseHeaderActionFilter(logger, "my-key-global", "my-value-global"));
            });

            services.AddHttpLogging(options =>
            {
                options.LoggingFields =
                Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties |
                Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;

            });
            //add services into IOC container
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IPersonService, PersonsService>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IPersonRepository, PersonsRepository>();
            services.AddTransient<PersonListActionFilter>();
            services.AddDbContext<PersonsDbContext>
                (
                    options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    }
                );
        }
    }
}
