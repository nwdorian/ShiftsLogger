using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Model;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _userService.GetAllAsync();

        if (response.Success)
        {
            var users = _mapper.Map<List<UserRead>>(response.Data);
            return Ok(users);
        }

        return BadRequest(response.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _userService.GetByIdAsync(id);

        if (response.Success)
        {
            var user = _mapper.Map<UserRead>(response.Data);
            return Ok(user);
        }

        return BadRequest(response.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreate userCreate)
    {
        var user = _mapper.Map<User>(userCreate);

        var response = await _userService.CreateAsync(user);

        if (response.Success)
        {
            var userRead = _mapper.Map<UserRead>(response.Data);
            return CreatedAtAction(nameof(GetById), new { Id = user.Id }, userRead);
        }

        return BadRequest(response.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _userService.DeleteAsync(id);

        if (response.Success)
        {
            return Ok(response.Message);
        }

        return BadRequest(response.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UserUpdate userUpdate)
    {
        var user = _mapper.Map<User>(userUpdate);

        var response = await _userService.UpdateAsync(id, user);

        if (response.Success)
        {
            return Ok(response.Message);
        }

        return BadRequest(response.Message);
    }
}
