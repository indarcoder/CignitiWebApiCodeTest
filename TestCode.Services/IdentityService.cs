using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestCode.Data;
using TestCode.Domain;
using TestCode.Options;


namespace TestCode.Services
{
    public class IdentityService : IIdentityService
    {       
        private readonly JwtSettings _jwtSettings;
        private readonly DataContext _context;
        private readonly TokenValidationParameters _tokenvalidationparameters;

        public IdentityService(JwtSettings jwtSettings, DataContext dataContext, TokenValidationParameters tokenValidationParameters)
        {          
            _jwtSettings = jwtSettings;           
            _context = dataContext;
            _tokenvalidationparameters = tokenValidationParameters;
        }      
        
       
        public async Task<AuthenticationResult> LoginAsync(string Username, string Password)
        {
            if(Username == "admin" && Password == "admin")
            {
                string userId = "f5a14454-54b2-4cda-8dbb-fe93ed765561";
                string email = "test" + Guid.NewGuid().ToString().Substring(0, 4) + "@test.com";
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, email),
                        new Claim(JwtRegisteredClaimNames.GivenName, Username),                       
                        new Claim("id", userId),                       
                }),
                    Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new AuthenticationResult
                {
                    Success = true,
                    Token = tokenHandler.WriteToken(token),                    
                };
            }
            else
            {
                return new AuthenticationResult
                {
                    Success = false
                };
            }
        }
       

        private ClaimsPrincipal GetPrincipalFromToken(string token, bool IsRefreshTokenRequest)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            try
            {
                if(IsRefreshTokenRequest)
                {
                    _tokenvalidationparameters.ValidateLifetime = false;
                }

                var principal = tokenhandler.ValidateToken(token, _tokenvalidationparameters, out var validationToken);

                if (!IsJwtWithValidSecurityAlgorithem(validationToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }
        private bool IsJwtWithValidSecurityAlgorithem(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
