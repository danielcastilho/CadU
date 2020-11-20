using CadU.Interfaces.Auth;

namespace CadU.Controllers
{
  using CadU.Models;
  using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/user")]
[ApiController]
[Authorize("Bearer")]
public sealed class UserController : ControllerBase
{
  private readonly ILoggedUserService _loggedUserService;

  public UserController(
      ILoggedUserService loggedUserService)
  {
    _loggedUserService = loggedUserService;
  }

  [HttpGet]
  public IActionResult Get()
  {
    var user = _loggedUserService.GetLoggedUser<User>();

    return Ok(user);
  }
}

}