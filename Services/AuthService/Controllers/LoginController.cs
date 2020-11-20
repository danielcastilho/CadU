using System.Threading.Tasks;
using CadU.Interfaces.Auth;
using CadU.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadU.Controllers
{
[Route("api/login")]
[ApiController]
public sealed class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly Interfaces.Auth.IAuthorizationService _authorizationService;
 
    public LoginController(
        IAuthenticationService authenticationService,
        Interfaces.Auth.IAuthorizationService authorizationService)
    {
        _authenticationService = authenticationService;
        _authorizationService = authorizationService;
    }
 
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] LoginUser loginUser)
    {
        var authorization = await _authorizationService.AuthorizeAsync(loginUser);
        if (!authorization.Success)
        {
            return Ok(authorization);
        }
 
        IAuthenticationResult authentication = await _authenticationService.AuthenticateAsync(authorization.Data);
        if (!authentication.Success)
        {
            return Ok(authentication);
        }
 
        return Ok(authentication);
    }
}
}