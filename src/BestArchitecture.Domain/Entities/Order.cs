using BestArchitecture.Domain.Common;

namespace BestArchitecture.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
