using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Model;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
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
            return NoContent();
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
            return NoContent();
        }

        return BadRequest(response.Message);
    }

    [HttpPut("{id}/shifts")]
    public async Task<IActionResult> UpdateShifts(Guid id, List<ShiftRead> shiftsRead)
    {
        var shifts = _mapper.Map<List<Shift>>(shiftsRead);

        var response = await _userService.UpdateShiftsAsync(id, shifts);

        if (response.Success)
        {
            return NoContent();
        }

        return BadRequest(response.Message);
    }

    [HttpGet("{id}/shifts")]
    public async Task<IActionResult> GetShifts(Guid id)
    {
        var response = await _userService.GetShiftsByUserIdAsync(id);

        if (response.Success)
        {
            var shifts = _mapper.Map<List<ShiftRead>>(response.Data);
            return Ok(shifts);
        }

        return BadRequest(response.Message);
    }
}
