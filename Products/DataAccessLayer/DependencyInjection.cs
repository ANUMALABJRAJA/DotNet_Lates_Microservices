using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer{
    public static class DependencyInjection{
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection service, IConfiguration configuration)
        {
            string connectionStringTemplate = configuration["ConnectionString:DefaultConnection"]!;
            string connectionString = connectionStringTemplate
            .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
            .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));


            service.AddDbContext<ApplicationDbContext>(options =>{
                options.UseMySQL(connectionString);
            });

            service.AddScoped<IProductRepository, ProductRepository>();
            return service;
        }
    }
}