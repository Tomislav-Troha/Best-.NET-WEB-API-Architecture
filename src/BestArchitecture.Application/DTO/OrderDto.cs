using BestArchitecture.Application.DTO.Base;

namespace BestArchitecture.Application.DTO
{
    public class OrderDto : BaseDto
    {
        public short? CarId { get; set; }

        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public CustomerDto? Customer { get; set; }
    }
}
