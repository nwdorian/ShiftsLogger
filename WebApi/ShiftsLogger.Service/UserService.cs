using ShiftsLogger.Model;
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
}
