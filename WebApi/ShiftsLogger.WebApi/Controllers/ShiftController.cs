﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Model;
using ShiftsLogger.Service.Common;
using ShiftsLogger.WebApi.RestModels;

namespace ShiftsLogger.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;
    private readonly IMapper _mapper;

    public ShiftController(IShiftService shiftService, IMapper mapper)
    {
        _shiftService = shiftService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _shiftService.GetAllAsync();

        if (response.Success)
        {
            var shifts = _mapper.Map<List<ShiftRead>>(response.Data);
            return Ok(shifts);
        }

        return BadRequest(response.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _shiftService.GetByIdAsync(id);

        if (response.Success)
        {
            var shift = _mapper.Map<ShiftRead>(response.Data);
            return Ok(shift);
        }

        return BadRequest(response.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ShiftCreate shiftCreate)
    {
        var shift = _mapper.Map<Shift>(shiftCreate);

        var response = await _shiftService.CreateAsync(shift);

        if (response.Success)
        {
            return CreatedAtAction(nameof(GetById), new { Id = shift.Id }, shift);
        }

        return BadRequest(response.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _shiftService.DeleteAsync(id);

        if (response.Success)
        {
            return Ok(response.Message);
        }

        return BadRequest(response.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ShiftUpdate shiftUpdate)
    {
        var shift = _mapper.Map<Shift>(shiftUpdate);

        var response = await _shiftService.UpdateAsync(id, shift);

        if (response.Success)
        {
            return Ok(response.Message);
        }

        return BadRequest(response.Message);
    }
}
