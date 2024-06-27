using Taxation.Services.Features.Calculations;
using Taxation.Services.Features.Information;

namespace Taxation.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<InfoProvider>();
            services.AddTransient<TaxCalculator>();
        }
    }
}
