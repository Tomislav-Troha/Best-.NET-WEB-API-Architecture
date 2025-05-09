namespace BestArchitecture.Application.DTO
{
    public class OrderDto : BaseDto
    {
        // Ako želimo dodatna polja specifična za Order:
        public short? CarId { get; set; }

        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
