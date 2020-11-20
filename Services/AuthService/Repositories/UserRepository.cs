using System.Collections.Generic;
using System.Linq;
using CadU.Interfaces.Auth;
using CadU.Interfaces.Repositories;
using CadU.Models;

namespace CadU.Repositories
{

  public class UserRepository : IUserRepository
  {
    public User Get(string username, string password)
    {
      var users = new List<User>();
      users.Add(new User { Id = "1", Name = "batman", Password = "batman", Role = "manager" });
      users.Add(new User { Id = "2", Name = "robin", Password = "robin", Role = "employee" });
      return users.Where(x => x.Name.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
    }
  }
}