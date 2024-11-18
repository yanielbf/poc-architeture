using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WROBoxLabelGeneration.SharedKernel.Logger;

namespace WROBoxLabelGeneration.SharedKernel
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernel(this IServiceCollection services)
            => services
                .AddLogger()
                .InitializeLogger();

        private static IServiceCollection AddLogger(this IServiceCollection services)
        {
            services.Configure<LoggerFilterOptions>(options =>
            {
                LoggerFilterRule toRemove = options.Rules.FirstOrDefault(
                    rule => rule.ProviderName == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider"
                );

                if (toRemove is not null)
                {
                    options.Rules.Remove(toRemove);
                }
            });
            services.AddLogging(logbuilder =>
            {
                logbuilder.AddSerilog(new LoggerConfiguration().WriteTo.Console().CreateLogger());
            });
            return services;
        }

        private static IServiceCollection InitializeLogger(this IServiceCollection services)
        {
            LoggerHelper.InitializeLogger(services.BuildServiceProvider().GetRequiredService<ILoggerFactory>());
            return services;
        }
    }
}
