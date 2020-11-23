using System.Threading.Tasks;
using CadU.AuthLibrary.Entities;

namespace CadU.Interfaces.Auth
{
public interface IAuthenticationService
{
    Task<IAuthenticationResult> AuthenticateAsync(
        User user);
}
}