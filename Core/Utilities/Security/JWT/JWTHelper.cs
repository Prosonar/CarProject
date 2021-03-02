using Core.Entity.Concrete;
using Core.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Security.JWT
{
    public class JWTHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JWTHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            //_tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            IConfigurationSection section = Configuration.GetSection(key: "TokenOptions");
            _tokenOptions = new TokenOptions()
            {
                Audience = section.GetSection("Audience").Value,
                Issuer = section.GetSection("Issuer").Value,
                SecurityKey = section.GetSection("SecurityKey").Value,
                AccessTokenExpiration = Convert.ToInt32(section.GetSection("AccessTokenExpiration").Value),
            };
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var singningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var jwtToken = CreateJWTSecurityToken(user, operationClaims, _tokenOptions, singningCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtToken);

            var accessToken = new AccessToken();

            accessToken.Token = token;
            accessToken.Expiration = _accessTokenExpiration;

            return accessToken;
        }
        public JwtSecurityToken CreateJWTSecurityToken(User user,List<OperationClaim> operationClaims,
            TokenOptions tokenOptions,SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken
                (
                    issuer:tokenOptions.Issuer,
                    audience:tokenOptions.Audience,
                    claims:SetClaims(user,operationClaims),
                    notBefore:DateTime.Now,
                    expires:_accessTokenExpiration,
                    signingCredentials:signingCredentials
                );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims)
        {
            var claim = new List<Claim>();
            claim.AddEmail(user.Email);
            claim.AddName(user.FirstName +" "+ user.LastName);
            claim.AddNameIdentifier(user.Id.ToString());
            if(operationClaims != null)
            {
                claim.AddRoles(operationClaims.Select(x => x.Name).ToArray());
            }
            return claim;
        }
    }
}
