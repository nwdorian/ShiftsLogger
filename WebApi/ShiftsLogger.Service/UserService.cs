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
        user!.IsActive = false;

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

        user.DateUpdated = DateTime.Now;

        return await _userRepository.UpdateAsync(user);
    }
}
