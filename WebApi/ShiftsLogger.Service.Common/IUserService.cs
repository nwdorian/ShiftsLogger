using ShiftsLogger.Model;

namespace ShiftsLogger.Service.Common;
public interface IUserService
{
    Task<ApiResponse<List<User>>> GetAllAsync();
    Task<ApiResponse<User>> GetByIdAsync(Guid id);
}
