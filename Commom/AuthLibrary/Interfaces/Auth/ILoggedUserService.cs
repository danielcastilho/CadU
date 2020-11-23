using CadU.Interfaces.Base;
using CadU.AuthLibrary.Entities;

namespace CadU.Interfaces.Auth
{
  public interface ILoggedUserService
  {
    IBaseResult<T> GetLoggedUser<T>() where T : User;
  }
}