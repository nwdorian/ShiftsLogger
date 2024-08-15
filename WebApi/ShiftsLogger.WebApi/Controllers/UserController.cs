using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _userService.GetAllAsync();

        if (response.Success)
        {
            var users = _mapper.Map<UserRead>(response.Data);
            return Ok(users);
        }

        return BadRequest(response.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var response = await _userService.GetByIdAsync(id);

        if (response.Success)
        {
            var user = _mapper.Map<UserRead>(response.Data);
            return Ok(user);
        }

        return BadRequest(response.Message);
    }
}
