using AutoMapper;
using BestArchitecture.Application.DTO;
using BestArchitecture.Domain.Entities;

namespace BestArchitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<Order, OrderDto>();
        }
    }
}
