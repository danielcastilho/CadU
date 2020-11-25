using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CadU.User.Application.Interfaces;
using CadU.AuthLibrary.Settings;
using CadU.UserService.Models;
using CadU.User.Models;
using CadU.General.Infrastructure.Core;
using CadU.AuthLibrary.Models;
using System.Threading.Tasks;

namespace CadU.UserService.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UsersController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
      _userService = userService;
      _mapper = mapper;
      _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
    {
      if (model is null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      var user = await _userService.Authenticate(model.Email, model.Password);

      if (user == null)
        return BadRequest(new { message = "Username or password is incorrect" });

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
          }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // return basic user info and authentication token
      return Ok(new UserTokenModel
      {
        Id = user.Id.ToString(),
        Token = tokenString,
        Expiration = tokenDescriptor.Expires.Value
      });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserModel model)
    {

      // map model to entity
      var user = _mapper.Map<CadU.User.Core.Entities.User>(model);

      try
      {
        // create user
        user = await _userService.Create(user, model.Password);
        var result = _mapper.Map<UserModel>(user);
        return Ok(result);
      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      var users = _userService.GetAll();
      var model = _mapper.Map<IList<UserModel>>(users);
      return Ok(model);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
      var user = await _userService.GetById(id);
      var model = _mapper.Map<UserModel>(user);
      return Ok(model);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] UserModel model)
    {
      // map model to entity and set id
      var user = _mapper.Map<CadU.User.Core.Entities.User>(model);
      user.Id = Guid.Parse(id);

      try
      {
        // update user 
        _userService.Update(user, model.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      _userService.Delete(id);
      return Ok();
    }
  }
}