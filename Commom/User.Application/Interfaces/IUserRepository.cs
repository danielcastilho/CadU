using System;
using System.Threading.Tasks;
using CadU.General.Infrastructure.Core;


namespace CadU.User.Application.Interfaces
{
  public interface IUserRepository : IGenericRepository<User.Core.Entities.User, Guid>
  {
    Task<CadU.User.Core.Entities.User> GetByEmailAsync(string email);
  }
}