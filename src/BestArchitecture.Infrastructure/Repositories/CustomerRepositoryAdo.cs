using BestArchitecture.Domain.Entities;
using BestArchitecture.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BestArchitecture.Infrastructure.Repositories
{
    public class CustomerAdoRepository : ICustomerRepository
    {
        private readonly string? _cs;
        public CustomerAdoRepository(IConfiguration cfg) => _cs = cfg.GetConnectionString("DefaultConnection");

        public async Task<Customer?> GetByIdAsync(int id)
        {
            await using var conn = new SqlConnection(_cs);
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "SELECT Id, Name FROM Customers WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var rdr = await cmd.ExecuteReaderAsync();
            if (!await rdr.ReadAsync()) return null;

            return new Customer()
            {
                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                Name = rdr.GetString(rdr.GetOrdinal("Name"))
            };
        }

        public async Task<List<Customer>> ListAsync()
        {
            var list = new List<Customer>();
            await using var conn = new SqlConnection(_cs);
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "SELECT Id, Name FROM Customers", conn);

            using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                var cust = new Customer()
                {
                    Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                    Name = rdr.GetString(rdr.GetOrdinal("Name"))
                };
                list.Add(cust);
            }
            return list;
        }

        public async Task Add(Customer customer)
        {
            using var conn = new SqlConnection(_cs);
            await conn.OpenAsync();
            using var cmd = new SqlCommand(@"
                INSERT INTO Customers (Name)
                VALUES (@Name);
                SELECT CAST(SCOPE_IDENTITY() AS INT);", conn);
            cmd.Parameters.AddWithValue("@Name", customer.Name);

            var id = await cmd.ExecuteScalarAsync();
            if(id != null)
            {
                customer.Id = (int)id;
            }
        }

        public async Task Update(Customer customer)
        {
            using var conn = new SqlConnection(_cs);
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "UPDATE Customers SET Name = @Name WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Name", customer.Name);
            cmd.Parameters.AddWithValue("@Id", customer.Id);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task Delete(Customer customer)
        {
            using var conn = new SqlConnection(_cs);
            await conn.OpenAsync();
            using var cmd = new SqlCommand(
                "DELETE FROM Customers WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", customer.Id);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
