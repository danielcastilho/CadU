using CadU.Interfaces.Base;

namespace CadU.Interfaces.Auth
{
  public interface IAuthenticationResult : IBaseResult<object>
  {
    bool Authenticated { get; set; }
    string Created { get; set; }
    string Expiration { get; set; }
    string AccessToken { get; set; }
  }
}