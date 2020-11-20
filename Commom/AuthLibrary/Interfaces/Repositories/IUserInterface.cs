using CadU.Interfaces.Auth;
using CadU.Models;

namespace CadU.Interfaces.Repositories
{
  public interface IUserRepository
  {
    User Get(string username, string password);
  }
}