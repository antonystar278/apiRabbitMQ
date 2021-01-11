using CompleteOperationWorker;
using Core.Interfaces.Core.Services;
using Core.Interfaces.Infrastructure;
using Core.Interfaces.Operations.Messaging.Send;
using Core.Services;
using Infrastructure.AppContext;
using Infrastructure.Repositories.Repositories.EFRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Operations.Messaging.Send.Sender;

namespace OperationHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
                    services.AddHostedService<Worker>();

                    services.AddTransient<IOperationRepository, OperationRepository>();
                    services.AddTransient<IOperationUpdateSender, OperationCreateCommandSender>();
                    services.AddTransient<IOperationService, OperationService>();
                });
    }
}
