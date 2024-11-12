﻿using ShiftsLogger.Model;

namespace ShiftsLogger.Repository.Common;
public interface IUserRepository
{
    Task<ApiResponse<List<User>>> GetAllAsync();
    Task<ApiResponse<User>> GetByIdAsync(Guid id);
    Task<ApiResponse<User>> CreateAsync(User user);
    Task<ApiResponse<User>> DeleteAsync(User user);
    Task<ApiResponse<User>> UpdateAsync(User user);
    Task<ApiResponse<List<User>>> CreateManyAsync(List<User> users);
    Task<ApiResponse<User>> ModifyShiftsAsync(Guid userId, List<Shift> shifts);
}
