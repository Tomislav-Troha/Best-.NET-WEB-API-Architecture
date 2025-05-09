using BestArchitecture.Domain.Entities;

namespace BestArchitecture.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int id);
        Task<List<Customer>> ListAsync();
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task Delete(Customer customer);
    }
}
