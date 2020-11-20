using System.Security.Claims;
using System.Text.Json;
using CadU.Implementation.Base;
using CadU.Interfaces.Auth;
using CadU.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using CadU.Infrastructure.Utils;
using CadU.Models;

namespace CadU.Services
{
  public sealed class IdentityLoggedUserService : ILoggedUserService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityLoggedUserService(
        IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public IBaseResult<T> GetLoggedUser<T>()
        where T : User
    {
      var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
      var result = new BaseResult<T>();

      if (identity?.IsAuthenticated ?? false)
      {
        result.Data = identity?.FindFirst("Data")?.Value.FromJson<T>();
        result.Success = result.Data != null;
        result.Message = identity?.FindFirst("Data")?.Value;
      }

      return result;
    }

  }
}