using ShiftsLogger.Model;
using ShiftsLogger.Model.DTOs;

namespace ShiftsLogger.Repository.Common;
public interface IUserRepository
{
    Task<ApiResponse<List<UserDto>>> GetAllAsync();
}
