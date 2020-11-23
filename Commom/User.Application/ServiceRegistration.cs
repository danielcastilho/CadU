using CadU.User.Application.Implementation;
using CadU.User.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CadU.User.Application
{
  public static class ServiceRegistration
  {
    public static void AddInfrastructure(this IServiceCollection services)
    {
      services.AddTransient<IUserRepository, UserRepository>();
      services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
  }
}