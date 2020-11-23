namespace CadU.User.Application.Interfaces
{
  public interface IUnitOfWork
  {
    IUserRepository UserRepository { get; }
  }
}