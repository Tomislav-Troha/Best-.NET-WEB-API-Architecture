using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BestArchitecture.Infrastructure.Persistance
{
    public class DapperContext
    {
        private readonly string? _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")?
                                              .Replace("__DefaultConnectionPassword__",
                                               Environment.GetEnvironmentVariable("DefaultConnectionPassword"));
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
