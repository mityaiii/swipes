using Microsoft.AspNetCore.Mvc;
using Swipes.Api.Extensions;
using Swipes.Bll.Entities;
using Swipes.Bll.Models;
using Swipes.Bll.Services.Interfaces;

namespace Swipes.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<long> AddAsync([FromBody]UserEntityV1 userEntityV1, CancellationToken token)
    {
        var userId = await _userService.AddAsync(userEntityV1, token);

        return userId;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync([FromRoute] long id, CancellationToken token)
    {
        var userEntity = await _userService.GetAsync(id, token);
        return Ok(userEntity.ToDto());
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] UserFilter userFilter, CancellationToken token)
    {
        var userEntities = await _userService.GetAsync(userFilter, token);

        return Ok(userEntities.Select(u => u.ToDto())
            .ToArray());
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UserEntityV1 userEntityV1, CancellationToken token)
    {
        await _userService.UpdateAsync(id, userEntityV1, token);

        return Ok();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> RemoveAsync([FromRoute] long id, CancellationToken token)
    {
        await _userService.RemoveAsync(id, token);
        
        return NoContent();
    }
}
