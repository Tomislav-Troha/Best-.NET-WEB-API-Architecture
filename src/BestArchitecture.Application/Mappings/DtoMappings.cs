using BestArchitecture.Application.DTO;
using BestArchitecture.Domain.Entities;

namespace BestArchitecture.Application.Mappings
{
    public static class DtoMappings
    {
        #region Customer
        public static CustomerDto ToDto(this Customer c)
        {
            var dto = new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                City = c.City
            };
            return dto;
        }
        #endregion

        #region Order
        public static OrderDto ToDto(this Order o)
        {
            var dto = new OrderDto
            {
                Id = o.Id,
                Name = o.Name,
                Address = o.Address,
                City = o.City,
                Email = o.Email,
                Customer = o.Customer?.ToDto(),
            };

            return dto;
        }
        #endregion
    }
}
