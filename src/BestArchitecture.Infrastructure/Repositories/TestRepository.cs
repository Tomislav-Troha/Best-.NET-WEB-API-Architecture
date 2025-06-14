using System.Data;
using BestArchitecture.Application.Repositories;
using BestArchitecture.Application.Repositories.Cache;
using BestArchitecture.Domain.Entities;
using BestArchitecture.Infrastructure.Persistance;
using Dapper;

namespace BestArchitecture.Infrastructure.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly DapperContext _db;
        private readonly IMemoryCacheRepository _cache;
        public TestRepository(DapperContext db, IMemoryCacheRepository cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task<Customer?> GetById(int id)
        {
            try
            {
                var key = $"GetCustomerById_{id}";
                if (await _cache.ExistsAsync<Customer>(key))
                {
                    return await _cache.GetAsync<Customer>(key);
                }

                using var conn = _db.CreateConnection();

                var result = await conn.QueryFirstOrDefaultAsync<Customer>(
                    sql: "GetCustomer",
                    param: new { Id = id },
                    commandType: CommandType.StoredProcedure);

                await _cache.SetAsync(key, result, TimeSpan.FromMinutes(30));

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}