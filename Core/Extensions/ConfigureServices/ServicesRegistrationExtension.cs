using Core.Interfaces.Core.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions.ConfigureServices
{
    public static class ServicesRegistrationExtension
    {
        public static IServiceCollection ConfigureCoreDependencies(this IServiceCollection services)
        {
            services.AddTransient<IOperationService, OperationService>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }

    }
}
