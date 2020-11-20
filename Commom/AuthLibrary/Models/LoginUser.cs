using CadU.Interfaces.Auth;

namespace CadU.Models
{
  public sealed class LoginUser
  {
    public string LoginOrEmail { get; set; }
    public string Password { get; set; }
  }
}