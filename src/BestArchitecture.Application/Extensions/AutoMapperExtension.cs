using AutoMapper;
using BestArchitecture.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace BestArchitecture.Application.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }

        public static TDestionation ToDto<TDestionation>(this object source, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(source);

            return mapper.Map<TDestionation>(source);
        }
    }
}
