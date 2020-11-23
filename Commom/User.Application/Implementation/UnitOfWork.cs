using CadU.User.Application.Interfaces;

namespace CadU.User.Application.Implementation
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly IUserRepository _userRepository;

    public UnitOfWork(IUserRepository userRepository)
    {
      this._userRepository = userRepository;
    }
    public IUserRepository UserRepository => _userRepository;
  }
}