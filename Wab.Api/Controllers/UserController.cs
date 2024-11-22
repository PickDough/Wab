using Microsoft.AspNetCore.Mvc;
using Wab.Core.Domain;
using Wab.Core.Service;

namespace Wab.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<User> GetById(Guid id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpGet("compound/{id:guid}")]
    public ActionResult<UserCompoundDto> GetCompoundUser(Guid id)
    {
        var userCompound = _userService.GetCompoundById(id);
        return Ok(userCompound);
    }
}