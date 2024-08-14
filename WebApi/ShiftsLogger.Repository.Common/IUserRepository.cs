using ShiftsLogger.Model;

namespace ShiftsLogger.Repository.Common;
public interface IUserRepository
{
    Task<ApiResponse<List<User>>> GetAllAsync();
}
