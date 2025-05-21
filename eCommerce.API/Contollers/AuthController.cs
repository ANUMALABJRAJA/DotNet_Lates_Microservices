using eCommerece.API.Core.ServiceContracts;
using eCommerece.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace eCommerece.API{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase{

        private readonly IUserService _userService;

        public AuthController(IUserService userService){
            _userService = userService;
        }
        [Route("register")]
        [HttpPost]
       public async Task<IActionResult> Register(RegisterRequest registerRequest){

            if(registerRequest == null)
            {
                return BadRequest("Invalid Registration Data");
            }

            AuthenticationResponse? authenticationResponse = await _userService.Register(registerRequest);

            if(authenticationResponse == null || authenticationResponse.Sucess == false)
            {
                return BadRequest(authenticationResponse);
            }

            return Ok(authenticationResponse);

        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest){
            if(loginRequest == null)
            {
                return BadRequest("Invalid Input/Data");
            }
            AuthenticationResponse? authenticationResponse = await _userService.Login(loginRequest);

            if(authenticationResponse == null || authenticationResponse.Sucess == false)
            {
                return Unauthorized(authenticationResponse);
            }

            return Ok(authenticationResponse);
        }

    }

}