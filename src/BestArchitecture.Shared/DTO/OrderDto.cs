﻿namespace BestArchitecture.Application.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public CustomerDto? Customer { get; set; }
    }
}
