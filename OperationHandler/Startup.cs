using Core.Interfaces.Core.Services;
using Core.Interfaces.Infrastructure;
using Core.Services;
using Infrastructure.AppContext;
using Infrastructure.Repositories.Repositories.EFRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OperationHandler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHostedService<CreateOperationWorker>();
            services.AddHostedService<CompleteOperationWorker>();

            services.AddTransient<IOperationRepository, OperationRepository>();
            services.AddTransient<IOperationService, OperationService>();
        }
    }
}
