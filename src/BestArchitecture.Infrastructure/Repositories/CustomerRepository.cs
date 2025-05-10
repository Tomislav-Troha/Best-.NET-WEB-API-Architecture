using System.Reflection;
using BestArchitecture.Domain.Entities;
using BestArchitecture.Domain.Repositories;
using BestArchitecture.Infrastructure.Persistance;
using Dapper;

namespace BestArchitecture.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _db;
        public CustomerRepository(DapperContext db) => _db = db;

        public async Task<Customer?> GetByIdAsync(int id)
        {
            //using var conn = _db.CreateConnection();
            //const string sql = "SELECT Id, Name FROM Customers WHERE Id = @Id";
            //return await conn.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });

            return new Customer
            {
                Id = id,
                Address = "Some Street",
                City = "New York",
                Name = "Little Stuart"
            };
        }

        public async Task<List<Customer>> ListAsync()
        {
            using var conn = _db.CreateConnection();
            const string sql = "SELECT Id, Name FROM Customers";
            var customers = await conn.QueryAsync<Customer>(sql);
            return customers.ToList();
        }

        public async Task Add(Customer customer)
        {
            // kod može biti i async, ali Dapper ne podržava direktno INSERT returning Id
            using var conn = _db.CreateConnection();

            const string sql = @"
                INSERT INTO Customers (Name)
                VALUES (@Name);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await conn.QueryFirstAsync<int>(sql, new { customer.Name });

            // postavi Id natrag u entitet
            typeof(Customer)
              .GetProperty("Id", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)!
              .SetValue(customer, id);
        }

        public async Task Update(Customer customer)
        {
            using var conn = _db.CreateConnection();
            const string sql = "UPDATE Customers SET Name = @Name WHERE Id = @Id";
            await conn.ExecuteAsync(sql, customer);
        }

        public async Task Delete(Customer customer)
        {
            using var conn = _db.CreateConnection();
            const string sql = "DELETE FROM Customers WHERE Id = @Id";
            await conn.ExecuteAsync(sql, new { customer.Id });
        }
    }
}