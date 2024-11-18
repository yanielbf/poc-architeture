using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WROBoxLabelGeneration.Application.Contracts.Persistence;
using WROBoxLabelGeneration.Persistence.DbContexts;
using WROBoxLabelGeneration.Persistence.Repositories;
using WROBoxLabelGeneration.SharedKernel.Configurations;

namespace WROBoxLabelGeneration.SharedKernel
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
            => services
                .AddEntityFramework()
                .AddInstanceToDI();

        private static IServiceCollection AddEntityFramework(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var appSettings = serviceProvider.GetService<AppSettings>();
            
            if (appSettings == null)
            {
                throw new ArgumentNullException("Appsettings was not load");
            }

            services.AddDbContext<ShipBobLiveDbContext>(
                db => db.UseSqlServer(appSettings.ConnectionStringsSettings.ShipbobLive),
                ServiceLifetime.Singleton
            );
            return services;
        }

        private static IServiceCollection AddInstanceToDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IWroRepository, WroRepository>();;
            return services;
        }
    }
}
