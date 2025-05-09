using BestArchitecture.Domain.Entities;

namespace BestArchitecture.Application.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public OrderDto? Order { get; set; }
    }
}
