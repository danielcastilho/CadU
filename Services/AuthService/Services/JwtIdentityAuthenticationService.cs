using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CadU.Implementation.Auth;
using CadU.Interfaces.Auth;
using Microsoft.IdentityModel.Tokens;
using CadU.Infrastructure.Utils;
using CadU.AuthLibrary.Entities;
using Microsoft.Extensions.Options;
using CadU.AuthLibrary.Settings;

namespace CadU.Services
{
  public sealed class JwtIdentityAuthenticationService : IAuthenticationService
  {
    private readonly AppSettings _appSettings;
    public JwtIdentityAuthenticationService(IOptions<AppSettings> appSettings)
    { _appSettings = appSettings.Value;
    }

    public async Task<IAuthenticationResult> AuthenticateAsync(
        User user)
    {
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id),
            new Claim("Data", user.ToJson())
        };

      var identity = new ClaimsIdentity(
          new GenericIdentity(user.Id, "Login"),
          claims);

      var created = DateTime.UtcNow;
      var expiration = created + TimeSpan.FromSeconds(60000);
      var handler = new JwtSecurityTokenHandler();
      var securityToken = handler.CreateToken(new SecurityTokenDescriptor
      {
        Issuer = "FSL",
        Audience = "FSL",
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Subject = identity,
        NotBefore = created,
        Expires = expiration
      });

      var dateFormat = "yyyy-MM-dd HH:mm:ss";
      var result = new AuthenticationResult
      {
        Success = true,
        Authenticated = true,
        Created = created.ToString(dateFormat),
        Expiration = expiration.ToString(dateFormat),
        AccessToken = handler.WriteToken(securityToken),
        Message = "OK"
      };
      await Task.Yield();
      return result;
    }
  }
}
