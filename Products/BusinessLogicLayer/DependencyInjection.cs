using BusinessLogicLayer.Mappers;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
            service.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
            service.AddScoped<IProductsService, ProductService>();
            return service;
        }
    }
}