using AutoMapper;
using CadU.AuthLibrary.Models;

namespace CadU.UserService.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<User.Core.Entities.User, LoginUserModel>();
    //   CreateMap<RegisterModel, User>();
    //   CreateMap<UpdateModel, User>();
    }
  }
}