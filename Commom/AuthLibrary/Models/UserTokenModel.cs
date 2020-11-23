using System;

namespace CadU.AuthLibrary.Models
{
  public class UserTokenModel
  {
    public string Id { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
  }
}
