using Core.Interfaces.Operations.Messaging.Send;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Operations.Messaging.Send.Options;
using Operations.Messaging.Send.Sender;

namespace OperationsMessagingSend.ConfigureServices
{
    public static class ServicesRegistrationExtension
    {
        public static IServiceCollection ConfigureMessagingSendDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMq"));
            services.AddTransient<IOperationUpdateSender, OperationCreateCommandSender>();

            return services;
        }
    }
}
