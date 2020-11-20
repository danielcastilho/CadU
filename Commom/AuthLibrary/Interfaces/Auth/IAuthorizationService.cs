using System.Threading.Tasks;
using CadU.Interfaces.Base;
using CadU.Models;

namespace CadU.Interfaces.Auth
{
  public interface IAuthorizationService
  {
    Task<IBaseResult<User>> AuthorizeAsync(
        LoginUser loginUser);
  }
}