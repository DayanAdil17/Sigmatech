using Microsoft.AspNetCore.Mvc;
using src.DTO.Login.Request;
using src.DTO.Login.Response;
using src.Services.Login.Query;

namespace src.Controllers
{
    [ApiController]
    [Route("")]
    public class LoginController : ControllerBase
    {
        private readonly LoginQuery loginQuery;
        public LoginController(LoginQuery loginQuery)
        {
            this.loginQuery = loginQuery;
            
        }

        [HttpGet("login")]
        [ActionName("")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await loginQuery.Login(request);
        }
    }
}