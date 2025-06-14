namespace BestArchitecture.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public Customer? Customer { get; set; }
    }
}
