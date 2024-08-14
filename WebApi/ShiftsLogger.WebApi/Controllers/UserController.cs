using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Service.Common;

namespace ShiftsLogger.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _userService.GetAllAsync();

        if (response.Success)
        {
            return Ok(response.Data);
        }

        return BadRequest(response.Message);
    }
}
