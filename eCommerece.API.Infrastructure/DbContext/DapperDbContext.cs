using System.Data;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace eCommerece.API.Infrastruture.DbContext{
    public class DapperDbContext{

        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("PostgreSqlConnection");
            string finalValue = connectionString
                                .Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST"))
                                .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSSOWRD"));

            // Creating connection
            _connection = new NpgsqlConnection(finalValue);

        }

        public IDbConnection DbConnection => _connection;
    }
}