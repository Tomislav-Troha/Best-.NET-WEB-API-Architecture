using BestArchitecture.Domain.Repositories;
using BestArchitecture.Infrastructure.Persistance;
using BestArchitecture.Infrastructure.Repositories;

namespace BestArchitecture.Api.Collections
{
    public static class InfrastrucutreCollections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            
            return services;
        }
    }
}
