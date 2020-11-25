using System;
using Xunit;
using Moq;
using CadU.User.Core.Entities;
using CadU.User.Application.Interfaces;
using CadU.User.Application.Implementation;
using CadU.UserServiceApi.Controllers;
using CadU.UserServiceApi.Helpers;
using AutoMapper;
using CadU.AuthLibrary.Settings;
using Microsoft.Extensions.Options;
using CadU.UserServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using CadU.User.Models;
using System.Threading.Tasks;

namespace Tests
{
  public class UserServiceTest
  {
    /// <summary>
    /// Teste com senha inválida
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async void Authenticate_NotOk()
    {
      var profile = new AutoMapperProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
      IMapper mapper = new Mapper(configuration);

      var mockRepo = new Mock<IUserRepository>();
      mockRepo.Setup(p => p.GetByEmailAsync("jota@jota.com.br")).ReturnsAsync(new User
      {
        Id = Guid.NewGuid(),
        Email = "jota@jota.com.br",
        Password = "xxxxxxx"
      });

      var mockOpt = new Mock<IOptions<AppSettings>>();
      mockOpt.Setup(opt => opt.Value).Returns(new AppSettings() { Secret = "fedaf7d8863b48e197b9287d492b708e" });

      UserService userService = new UserService(mockRepo.Object);
      UsersController controller = new UsersController(userService, mapper, mockOpt.Object);
      var authenticateModel = new AuthenticateModel()
      {
        Email = "jota@jota.com.br",
        Password = "123456"
      };
      var response = await controller.Authenticate(authenticateModel);
      Assert.True(response is BadRequestObjectResult);
      var badRequest = response as BadRequestObjectResult;
      var message = badRequest.Value.GetType().GetProperty("message").GetValue(badRequest.Value);
      Assert.True(message.Equals("Username or password is incorrect"));
    }

    /// <summary>
    /// Teste com senha correta
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async void Authenticate_Ok()
    {
      var profile = new AutoMapperProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
      IMapper mapper = new Mapper(configuration);

      var mockRepo = new Mock<IUserRepository>();
      mockRepo.Setup(p => p.GetByEmailAsync("jota@jota.com.br")).ReturnsAsync(new User
      {
        Id = Guid.NewGuid(),
        Email = "jota@jota.com.br",
        Password = "xxxxxxx"
      });

      var mockOpt = new Mock<IOptions<AppSettings>>();
      mockOpt.Setup(opt => opt.Value).Returns(new AppSettings() { Secret = "fedaf7d8863b48e197b9287d492b708e" });

      UserService userService = new UserService(mockRepo.Object);
      UsersController controller = new UsersController(userService, mapper, mockOpt.Object);
      var authenticateModel = new AuthenticateModel()
      {
        Email = "jota@jota.com.br",
        Password = "xxxxxxx"
      };
      var response = await controller.Authenticate(authenticateModel);
      Assert.True(response is OkObjectResult);
      var okRequest = response as OkObjectResult;
      Assert.IsType<CadU.AuthLibrary.Models.UserTokenModel>(okRequest.Value);

    }

    /// <summary>
    /// Teste com senha correta
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async void Register_Ok()
    {
      var profile = new AutoMapperProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
      IMapper mapper = new Mapper(configuration);

      var mockRepo = new Mock<IUserRepository>();
      mockRepo.Setup(p => p.GetByEmailAsync("jota@jota.com.br")).Returns(Task.FromResult<User>(null));

      var mockOpt = new Mock<IOptions<AppSettings>>();
      mockOpt.Setup(opt => opt.Value).Returns(new AppSettings() { Secret = "fedaf7d8863b48e197b9287d492b708e" });

      UserService userService = new UserService(mockRepo.Object);

      UsersController controller = new UsersController(userService, mapper, mockOpt.Object);
      var userModel = new UserModel()
      {
        Email = "jota@jota.com.br",
        Password = "xxxxxxx",
        IsAdmin = false,
        Role = "user"
      };
      var response = await controller.Register(userModel);
      Assert.True(response is OkObjectResult);
      var okRequest = response as OkObjectResult;
      Assert.IsType<UserModel>(okRequest.Value);

    }

    /// <summary>
    /// Teste com senha correta
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async void Register_UserAlreadExists()
    {
      var profile = new AutoMapperProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
      IMapper mapper = new Mapper(configuration);

      var mockRepo = new Mock<IUserRepository>();
      mockRepo.Setup(p => p.GetByEmailAsync("jota@jota.com.br")).ReturnsAsync(new User
      {
        Id = Guid.NewGuid(),
        Email = "jota@jota.com.br",
        Password = "xxxxxxx"
      });

      var mockOpt = new Mock<IOptions<AppSettings>>();
      mockOpt.Setup(opt => opt.Value).Returns(new AppSettings() { Secret = "fedaf7d8863b48e197b9287d492b708e" });

      UserService userService = new UserService(mockRepo.Object);

      UsersController controller = new UsersController(userService, mapper, mockOpt.Object);
      var userModel = new UserModel()
      {
        Email = "jota@jota.com.br",
        Password = "xxxxxxx",
        IsAdmin = false,
        Role = "user"
      };
      var response = await controller.Register(userModel);
      Assert.True(response is BadRequestObjectResult);
      var badRequest = response as BadRequestObjectResult;
      var message = badRequest.Value.GetType().GetProperty("message").GetValue(badRequest.Value);
      Assert.True(message.Equals("Username \"jota@jota.com.br\" is already taken"));

    }

    /// O Restantes dos testes serão feitos muito em breve

  }
}
