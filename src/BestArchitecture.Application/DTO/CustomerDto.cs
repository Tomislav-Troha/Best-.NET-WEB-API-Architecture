namespace BestArchitecture.Application.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public List<OrderDto>? Orders { get; set; } = new List<OrderDto>();
    }
}
