using AutoMapper;
using eCommerece.API.Core.Entities;
using eCommerece.API.DTO;

namespace eCommerece.API.Core.Mapper{
    public class ApplicationUserMappingProfile : Profile{
        public ApplicationUserMappingProfile(){
            CreateMap<ApplicationUser, AuthenticationResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(des => des.PersonName, opt => opt.MapFrom(src => src.PersonName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Sucess, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore());
            
        }
    }
}