using System.Threading.Tasks;
using CadU.General.DapperORM;
using Microsoft.Extensions.Configuration;
using System;
using Dapper;

namespace CadU.User.Application.Implementation
{
  public class UserRepository : GenericRepository<User.Core.Entities.User, Guid>, Interfaces.IUserRepository
  {
    
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    static UserRepository()
    {
        CadU.User.Application.Mappers.EntitiesMap.Initialize();
    }


    public async Task<Core.Entities.User> GetByEmailAsync(string email)
    {
      return await Connection.QueryFirstOrDefaultAsync<User.Core.Entities.User>(
        "select * from \"User\" where \"Email\" = :email",
        new { email = email });
    }

    public async override Task<Core.Entities.User> GetByIdAsync(Guid id)
    {
      return await Connection.QuerySingleOrDefaultAsync<User.Core.Entities.User>("select * from \"User\" where \"Id\" = :id", new { id = id });
    }
  }
}