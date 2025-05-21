using AutoMapper;
using eCommerece.API.Core.Entities;
using eCommerece.API.Core.ServiceContracts;
using eCommerece.API.DTO;

namespace eCommerece.API.Core.Services{
    public class UserServices : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
        {
           ApplicationUser? user =  await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

          if(user == null)
          {
            return null;
          }
          //return new AuthenticationResponse(user.UserId, user.Email, user.PersonName ,user.Password, user.Gender, "Token", Sucess: true );
          return _mapper.Map<AuthenticationResponse>(user) with {Sucess = true, Token = "Token"};
        }

        public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
        {
            ApplicationUser user = new ApplicationUser(){PersonName = registerRequest.PersonName, Email = registerRequest.Email,Password = registerRequest.Password,Gender = registerRequest.Gender.ToString()};
            ApplicationUser? registeredUser = await _userRepository.AddUser(user);
            if(registeredUser == null)
            {
                return null;
            }

            //return new AuthenticationResponse(registeredUser.UserId, registeredUser.Email, registeredUser.PersonName, registeredUser.Password,registeredUser.Gender, "Token",Sucess: true);
            return _mapper.Map<AuthenticationResponse>(registeredUser) with {Sucess = true, Token = "Token"};
        }
    }
}