using BestArchitecture.Application.DTO;
using BestArchitecture.Domain.Entities;

namespace BestArchitecture.Application.Mappings
{
    public static class DtoMappings
    {
        #region CustomerDTO
        public static CustomerDto ToDto(this Customer c, IEnumerable<Order>? orders = null)
        {
            var dto = new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                City = c.City,
                Orders = orders?.Select(o => new OrderDto
                {
                    Id = o.Id,
                    Code = o.Code,
                    Name = o.Name,
                    Address = o.Address,
                    City = o.City,
                    Email = o.Email,
                    CustomerId = o.CustomerID,
                }).ToList()
            };
            return dto;
        }
        #endregion

        #region OrderDTO
        public static OrderDto ToDto(this Order o)
        {
            var dto = new OrderDto
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Address = o.Address,
                City = o.City,
                Email = o.Email,
                CustomerId = o.CustomerID
            };

            return dto;
        }
        #endregion
    }
}
