using CadU.Interfaces.Auth;

namespace CadU.AuthLibrary.Entities
{
  public class User
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool IsAdmin { get; set; }
  }
}