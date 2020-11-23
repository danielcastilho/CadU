

namespace CadU.User.Application.Implementation
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using CadU.General.Infrastructure.Core;
  using CadU.User.Application.Interfaces;
  using CadU.User.Core.Entities;
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
      this._userRepository = userRepository;
    }
    public async Task<User> Authenticate(string username, string password)
    {
      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        return null;

      var user = await _userRepository.GetByEmailAsync(username);

      // check if username exists
      if (user == null)
        return null;

      // check if password is correct
      if (password != user.Password)
        return null;

      // authentication successful
      return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
      return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetById(string id)
    {
      return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> Create(User user, string password)
    {
      // validation
      if (string.IsNullOrWhiteSpace(password))
        throw new AppException("Password is required");

      if (await _userRepository.GetByEmailAsync(user.Email) != null)
        throw new AppException("Username \"" + user.Email + "\" is already taken");

      await _userRepository.AddAsync(user);

      return user;
    }

    public async void Update(User userParam, string password = null)
    {
      var user = await _userRepository.GetByIdAsync(userParam.Id);

      if (user == null)
        throw new AppException("User not found");

      // update username if it has changed
      if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
      {
        // throw error if the new username is already taken
        if (await _userRepository.GetByEmailAsync(userParam.Email) != null)
          throw new AppException("Username " + userParam.Email + " is already taken");

        user.Email = userParam.Email;
      }

      // update user properties if provided
      if (!string.IsNullOrWhiteSpace(userParam.Name))
        user.Name = userParam.Name;

      // update password if provided
      if (!string.IsNullOrWhiteSpace(password))
      {
        user.Password = userParam.Password;
      }

      var ok = await _userRepository.UpdateAsync(user);
      if (!ok)
      {
        throw new AppException($"Cannot update this user {userParam.Email}");
      }

    }

    public async void Delete(string id)
    {
      var user = await _userRepository.GetByIdAsync(id);
      if (user != null)
      {
        await _userRepository.DeleteAsync(user);
      }
    }

  }
}