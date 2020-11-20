using CadU.Implementation.Base;

namespace CadU.Implementation.Auth
{
  public sealed class AuthenticationResult : BaseResult<object>, Interfaces.Auth.IAuthenticationResult
  {
    public bool Authenticated { get; set; }
    public string Created { get; set; }
    public string Expiration { get; set; }
    public string AccessToken { get; set; }
  }
}