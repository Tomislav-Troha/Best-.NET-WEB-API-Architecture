namespace BestArchitecture.Api.DependencyInjection
{
    public static class DependencyInjection
    {
        //Don't touch order
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<DapperContext>();
            services.AddSingleton<IMemoryCacheRepository, MemoryCacheRepository>();

            services.AddScoped<ITestRepository, TestRepository>();


            return services;
        }
    }
}
