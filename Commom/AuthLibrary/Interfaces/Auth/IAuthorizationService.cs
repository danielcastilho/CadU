using System.Threading.Tasks;
using CadU.Interfaces.Base;
using CadU.AuthLibrary.Entities;
using CadU.AuthLibrary.Models;

namespace CadU.Interfaces.Auth
{
  public interface IAuthorizationService
  {
    Task<IBaseResult<User>> AuthorizeAsync(
        LoginUserModel loginUser);
  }
}