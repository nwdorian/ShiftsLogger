﻿using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;
using ShiftsLogger.Service.Common;

namespace ShiftsLogger.Service;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<List<User>>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<ApiResponse<User>> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<ApiResponse<User>> CreateAsync(User user)
    {
        user.Id = Guid.NewGuid();
        user.IsActive = true;
        user.DateCreated = DateTime.Now;
        user.DateUpdated = DateTime.Now;

        return await _userRepository.CreateAsync(user);
    }

    public async Task<ApiResponse<User>> DeleteAsync(Guid id)
    {
        var response = await _userRepository.GetById(id);

        if (response.Success == false)
        {
            response.Message = "User not found!";
            return response;
        }

        var user = response.Data;

        if (user is null)
        {
            response.Message = "User is null";
            return response;
        }

        user.IsActive = false;

        return await _userRepository.DeleteAsync(user);
    }

    public async Task<ApiResponse<User>> UpdateAsync(Guid id, User user)
    {
        var response = await _userRepository.GetById(id);

        if (response.Success == false)
        {
            response.Message = "User not found!";
            return response;
        }

        var existingUser = response.Data;

        if (existingUser is null)
        {
            response.Message = "User is null";
            return response;
        }

        if (user.ShiftId != Guid.Empty)
        {
            existingUser.ShiftId = user.ShiftId;
        }

        if (!string.IsNullOrEmpty(user.FirstName))
        {
            existingUser.FirstName = user.FirstName;
        }

        if (!string.IsNullOrEmpty(user.LastName))
        {
            existingUser.LastName = user.LastName;
        }

        if (!string.IsNullOrEmpty(user.Email))
        {
            existingUser.Email = user.Email;
        }

        existingUser.DateUpdated = DateTime.Now;

        return await _userRepository.UpdateAsync(existingUser);
    }
}
