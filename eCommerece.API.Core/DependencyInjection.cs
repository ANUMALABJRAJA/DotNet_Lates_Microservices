using eCommerece.API.Core.ServiceContracts;
using eCommerece.API.Core.Services;
using eCommerece.API.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerece.API.Core{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            // Add Dependencies
            services.AddTransient<IUserService, UserServices>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidators>();
             return services;
        }
    }
}