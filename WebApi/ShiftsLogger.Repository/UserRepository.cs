using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL;
using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;

namespace ShiftsLogger.Repository;
public class UserRepository : IUserRepository
{
    private readonly ShiftsContext _context;
    private readonly IMapper _mapper;

    public UserRepository(ShiftsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<User>>> GetAllAsync()
    {
        var response = new ApiResponse<List<User>>();

        try
        {
            var users = await _context.Users.Where(u => u.IsActive == true).ToListAsync();

            if (users.Count == 0)
            {
                response.Message = "No users found in the database!";
                response.Success = false;
            }
            else
            {
                response.Data = _mapper.Map<List<User>>(users);
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository GetAllAsync: {ex.Message}";
            response.Success = false;
        }

        return response;
    }

    public async Task<ApiResponse<User>> GetById(Guid id)
    {
        var response = new ApiResponse<User>();

        try
        {
            var user = await _context.Users.Where(u => u.IsActive == true).SingleOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                response.Message = $"User with Id {id} doesn't exist";
                response.Success = false;
            }
            else
            {
                response.Data = _mapper.Map<User>(user);
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository GetById: {ex.Message}";
            response.Success = false;
        }
        return response;
    }
}
