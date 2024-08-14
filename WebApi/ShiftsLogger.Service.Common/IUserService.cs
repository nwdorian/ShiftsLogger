using ShiftsLogger.Model;
using ShiftsLogger.Model.DTOs;

namespace ShiftsLogger.Service.Common;
public interface IUserService
{
    Task<ApiResponse<List<UserDto>>> GetAllAsync();
}
