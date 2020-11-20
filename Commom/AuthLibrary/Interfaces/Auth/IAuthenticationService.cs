using System.Threading.Tasks;
using CadU.Models;

namespace CadU.Interfaces.Auth
{
public interface IAuthenticationService
{
    Task<IAuthenticationResult> AuthenticateAsync(
        User user);
}
}