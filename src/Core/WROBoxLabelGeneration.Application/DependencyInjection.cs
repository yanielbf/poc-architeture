using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WROBoxLabelGeneration.Application.Pipelines;

namespace WROBoxLabelGeneration.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services
                .AddFluentValidation()
                .AddMediator();

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(Assembly.Load("WROBoxLabelGeneration.Application"));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            
            return services;
        }

        private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssembly(Assembly.Load("WROBoxLabelGeneration.Application"));
        }
    }
}
