using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using src.DTO.Login.Response;

namespace src.Services.User.Query
{
    public class UserInfoQuery
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserInfoQuery(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            
        }

        public UserInfoResponse GetUserInfoFromToken()
        {
            var token = GetTokenFromRequest();
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token is missing.");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            // Extract claims from the token
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var userRole = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Return user info as a DTO or an object
            return new UserInfoResponse
            {
                userId = Guid.Parse(userId),
                userName = userName,
                userRole = userRole
            };
        }

        // Method to get the token from the request headers
        private string GetTokenFromRequest()
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            
            // If the header is present, extract the token
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                // Remove "Bearer " prefix if present
                if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return authorizationHeader.Substring("Bearer ".Length).Trim();
                }
                // If "Bearer" is not included, you can return the token directly
                return authorizationHeader; // Assuming it's the token
            }
            
            return null;
        }
    }
}