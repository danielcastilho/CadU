namespace CadU.User.Application.Interfaces
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using CadU.User.Core.Entities;
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(string id);
    }
}