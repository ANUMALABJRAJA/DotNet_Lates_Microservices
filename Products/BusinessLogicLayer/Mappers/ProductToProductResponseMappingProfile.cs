using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappers{
    public class ProductToProductResponseMappingProfile : Profile{
        public ProductToProductResponseMappingProfile(){
            CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ProductName , option => option.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category , option => option.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice , option => option.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock , option => option.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.ProductId , option => option.MapFrom(src => src.ProductId));
        }
    }   
}