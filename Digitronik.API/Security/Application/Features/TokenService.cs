using System.Security.Claims;
using System.Text;
using Digitronik.API.Security.Application.Constants;
using Digitronik.API.Security.Domain.Models.Aggregates;
using Digitronik.API.Security.Domain.Services;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Digitronik.API.Security.Application.Features;

public class TokenService : ITokenService
{
    private readonly string _key = "3a8f9c3e-6b4f-4b68-ae1e-284ba324dbfe";
    private readonly int _durationInMinutes = 360;
    
    public string GenerateToken(User user)
    {
        var claims = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new Claim(CustomClaimTypes.Username, user.Username),
        });
        
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(_durationInMinutes),
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
        
    }

    public async Task<int?> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return null;
        
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_key);
        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            });

            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
            var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sid).Value);
            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}