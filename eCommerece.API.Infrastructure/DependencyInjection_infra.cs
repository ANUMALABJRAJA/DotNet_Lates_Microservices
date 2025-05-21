using eCommerece.API.Core;
using eCommerece.API.Infrastruture.DbContext;
using eCommerece.API.Infrastruture.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerece.API.Infrastruture{
    public static class DependencyInjection_infra
    {
        public static IServiceCollection AddInfrastruture(this IServiceCollection services)
        {
            // Add Dependencies
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<DapperDbContext>();

             return services;
        }
    }
}