﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Model;
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

    [HttpGet("{id}")]
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

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCreate userCreate)
    {
        var user = _mapper.Map<User>(userCreate);

        var response = await _userService.CreateAsync(user);

        if (response.Success)
        {
            return CreatedAtAction("GetById", new { Id = user.Id }, user);
        }

        return BadRequest(response.Message);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var response = await _userService.DeleteAsync(id);

        if (response.Success)
        {
            return Ok(response.Message);
        }

        return BadRequest(response.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UserUpdate userUpdate)
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
