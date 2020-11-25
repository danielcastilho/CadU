using AutoMapper;
using CadU.AuthLibrary.Models;
using CadU.User.Models;

namespace CadU.UserServiceApi.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<User.Core.Entities.User, LoginUserModel>();
      CreateMap<LoginUserModel, User.Core.Entities.User>();
      CreateMap<User.Core.Entities.User, UserModel>();
      CreateMap<UserModel, User.Core.Entities.User>();

    }
  }
}