using System;
using System.Threading.Tasks;
using CadU.Implementation.Base;
using CadU.Interfaces.Auth;
using CadU.Interfaces.Base;
using CadU.Models;

namespace CadU.Tests.Fakes
{
  public sealed class FakeAuthorizationService : IAuthorizationService
  {
    public async Task<IBaseResult<User>> AuthorizeAsync(
        LoginUser loginUser)
    {
      var loginOrEmail = loginUser?.LoginOrEmail ?? "";
      var password = loginUser?.Password ?? "";

      var result = new BaseResult<User>();

      if (loginOrEmail == "teste" && password == "1234")
      {
        result.Success = true;
        result.Message = "User authorized!";
        result.Data = new User
        {
          Id = Guid.NewGuid().ToString(),
          Name = "Name test",
          Role = "User",
          IsAdmin = false
        };
      }
      else
      {
        result.Success = false;
        result.Message = "Not authorized!";
      }

      return await Task.FromResult(result);
    }
  }
}