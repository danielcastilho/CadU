using CadU.Interfaces.Base;
using CadU.Models;

namespace CadU.Interfaces.Auth
{
  public interface ILoggedUserService
  {
    IBaseResult<T> GetLoggedUser<T>() where T : User;
  }
}