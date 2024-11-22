using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WROBoxLabelGeneration.Application.Contracts.Infrastructure.HttpClients;
using WROBoxLabelGeneration.Application.Contracts.Infrastructure.LabelServices;
using WROBoxLabelGeneration.Infrastructure.HttpClients;
using WROBoxLabelGeneration.Infrastructure.Services;
using WROBoxLabelGeneration.Infrastructure.Services.LabelGenerators;
using WROBoxLabelGeneration.Infrastructure.Services.PdfGenerators;
using WROBoxLabelGeneration.SharedKernel.Configurations;

namespace WROBoxLabelGeneration.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services
                .AddHttpClients()
                .AddServices()
                .AddHtmlRenderer();

        private static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var appSettings = serviceProvider.GetService<AppSettings>();

            if(appSettings == null)
            {
                throw new ArgumentNullException("Appsettings was not load");
            }

            var tokenClientName = "IdentityServer";
            services.AddClientAccessTokenManagement(options =>
                options.Clients.Add(tokenClientName, new ClientCredentialsTokenRequest
                {
                    RequestUri = new Uri(appSettings.AuthClientSettings.AuthAddress),
                    ClientId = appSettings.AuthClientSettings.AuthClientId,
                    ClientSecret = appSettings.AuthClientSettings.AuthClientSecret,
                    Scope = appSettings.HttpClientSettings.Scopes(),
                }))
                .ConfigureBackchannelHttpClient();
            services
              .AddScoped<AttachmentsProxy>()
              .AddHttpClient<IAttachmentsProxy, AttachmentsProxy>(client =>
              {
                  client.BaseAddress = new Uri(appSettings.HttpClientSettings.AttachmentApi?.BaseUrl);
                  client.DefaultRequestHeaders.Add("Accept", "application/json");
              })
              .AddClientAccessTokenHandler(tokenClientName);
            services
                   .AddScoped<LabelingProxy>()
                   .AddHttpClient<ILabelingProxy, LabelingProxy>(client =>
                   {
                       client.BaseAddress = new Uri(appSettings.HttpClientSettings.LabelingApi?.BaseUrl);
                       client.DefaultRequestHeaders.Add("Accept", "application/json");
                   })
                   .AddClientAccessTokenHandler(tokenClientName);
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<GeneratorQrBarcodeService>();

            // Label Generators
            services.AddTransient<WroLabelGenerator>();
            services.AddTransient<Func<string, ILabelGenerator>>(serviceProvider => key =>
            {
                return key switch
                {
                    "wro-label-generator" => serviceProvider.GetService<WroLabelGenerator>() ?? throw new KeyNotFoundException($"No class WroLabelGenerator found"),
                    _ => throw new KeyNotFoundException($"No label generator found for key {key}")
                };
            });
            
            // Pdf Generators
            services.AddTransient<IronPdfGenerator>();
            services.AddTransient<SelectPdfGenerator>();
            services.AddTransient<PuppeterPdfGenerator>();
            services.AddTransient<BasePdfGenerator>(serviceProvider =>
            {
                var appSettings = serviceProvider.GetService<AppSettings>();

                if (appSettings == null || appSettings.LibraryInfrastructure.PdfGenerator == null)
                {
                    throw new ArgumentNullException("Appsettings was not load");
                }

                var key = appSettings.LibraryInfrastructure.PdfGenerator;

                return key switch
                {
                    "iron-pdf" => serviceProvider.GetService<IronPdfGenerator>() ?? throw new KeyNotFoundException($"No class IronPdfGenerator found"),
                    "select-pdf" => serviceProvider.GetService<SelectPdfGenerator>() ?? throw new KeyNotFoundException($"No class SelectPdfGenerator found"),
                    "puppeter-pdf" => serviceProvider.GetService<PuppeterPdfGenerator>() ?? throw new KeyNotFoundException($"No class PuppeterPdfGenerator found"),
                    _ => throw new KeyNotFoundException($"No pdf generator found for key {key}")
                };
            });

            return services;
        }

        private static IServiceCollection AddHtmlRenderer(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            HtmlRenderer render = new HtmlRenderer(provider, provider.GetRequiredService<ILoggerFactory>());
            services.AddSingleton(render);
            return services;
        }
    }
}
