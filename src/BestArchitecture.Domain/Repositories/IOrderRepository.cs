using BestArchitecture.Domain.Entities;

namespace BestArchitecture.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>>? GetAllOrdersByCustomer(int customerId);
    }
}
