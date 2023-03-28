using AML.Application.Features.Implementation;
using AML.Application.Features.Interface;
using AML.ExternalApi.ApiClient;
using Microsoft.Extensions.DependencyInjection;


namespace AML.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAMLPersonChecker, AMLPersonChecker>();
            services.AddHttpClient<ITronApiClient,TronApiClient>();

            return services;
        }
    }
}
