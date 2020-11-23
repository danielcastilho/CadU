using System.Threading.Tasks;
using CadU.General.Dapper;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace CadU.User.Application.Implementation
{
  public class UserRepository : GenericRepository<User.Core.Entities.User, string>, Interfaces.IUserRepository
  {
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Core.Entities.User> GetByEmailAsync(string email)
    {
      return await Connection.QueryFirstOrDefaultAsync<User.Core.Entities.User>(
        "select * from User where Email = @Email", new { Email = email });
    }
  }
}