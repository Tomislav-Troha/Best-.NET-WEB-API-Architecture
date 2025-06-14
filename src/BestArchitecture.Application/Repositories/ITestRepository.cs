using BestArchitecture.Domain.Entities;

namespace BestArchitecture.Application.Repositories
{
    public interface ITestRepository
    {
        Task<Customer?> GetById(int id);
    }
}
