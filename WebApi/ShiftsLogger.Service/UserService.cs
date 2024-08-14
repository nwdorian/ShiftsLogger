﻿using ShiftsLogger.Model;
using ShiftsLogger.Model.DTOs;
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

    public async Task<ApiResponse<List<UserDto>>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}
