using Core.Entities;
using Core.Interfaces.Infrastructure;
using Infrastructure.AppContext;
using Infrastructure.Repositories.Repositories.EFRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ConfigureServices
{
    public static class ServicesRegistrationExtension
    {
        public static IServiceCollection ConfigureInfrastructureDependencies(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IOperationRepository, OperationRepository>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }

}
