using BestArchitecture.Domain.Entities;
using BestArchitecture.Domain.Repositories;
using BestArchitecture.Infrastructure.Persistance;

namespace BestArchitecture.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _db;

        public OrderRepository(DapperContext db) => _db = db;
        public async Task<List<Order>>? GetAllOrdersByCustomer(int customerId)
        {
            // Simulacija vraćanja podataka
            List<Order> orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerID = 1
                },
                new Order
                {
                    Id = 2,
                    CustomerID = 1,
                    Address = "Some"
                },

                new Order
                {
                    Id = 2,
                    CustomerID = 2,
                    Address = "Amazon"
                }
            };

            return orders.Where(x => x?.CustomerID == customerId).ToList();

        }
    }
}
