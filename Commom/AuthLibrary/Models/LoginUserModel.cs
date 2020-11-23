using CadU.Interfaces.Auth;

namespace CadU.AuthLibrary.Models
{
  public sealed class LoginUserModel
  {
    public string LoginOrEmail { get; set; }
    public string Password { get; set; }
  }
}