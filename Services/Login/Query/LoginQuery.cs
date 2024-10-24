using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Sigmatech.Exceptions.Global;
using Sigmatech.Interfaces.UnitOfWork;
using src.DTO.Login.Request;
using src.DTO.Login.Response;
using src.Entities.User;

namespace src.Services.Login.Query
{
    public class LoginQuery
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public LoginQuery(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            
        }

        public async Task<LoginResponse> Login (LoginRequest request)
        {
            var user = await unitOfWork.userRepository.FindSingleOrDefault(x => x.userName == request.userName && x.password == request.password);
            if(user == null)
            {
                throw new NotFoundGlobalException("User", "USER", "userName", request.userName);
            }

            var response = new LoginResponse();

            response.token = GenerateJwtToken(user);

            return response;
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.userName),   // Add userName as a claim
                new Claim("id", user.id.ToString()),                 // Custom claim for User ID
                new Claim(ClaimTypes.Role, user.userRole),               // Add user role as a claim
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())  // Token ID
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(6), // Token expires in 1 hour
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}