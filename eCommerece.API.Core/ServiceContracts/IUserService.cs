using eCommerece.API.DTO;

namespace eCommerece.API.Core.ServiceContracts{

    public interface IUserService{
        Task<AuthenticationResponse?> Login(LoginRequest loginRequest);
        
        Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);

    }

}