using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StockMarket.Model.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.API.Common
{
    /// <summary>
    /// class to implement the interface <see cref="ITokenService"/>
    /// </summary>
    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;
        private readonly ILogger<TokenService> _logger;

        /// <summary>
        /// Constructor for TokenService
        /// </summary>
        /// <param name="logger">The logger</param>
        public TokenService(ILogger<TokenService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<inheritdoc/>
        public string BuildToken(string key, string issuer, User user)
        {
            try
            {
                var claims = new[] {
                    new Claim("Id", user.UserId),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("UserName", user.UserName),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                    expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        ///<inheritdoc/>
        public bool ValidateToken(string key, string issuer, string audience, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
            return true;
        }
        
    }

}
