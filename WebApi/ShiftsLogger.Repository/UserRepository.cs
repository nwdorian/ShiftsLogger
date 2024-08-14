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
            var users = await _context.Users.ToListAsync();

            response.Data = _mapper.Map<List<User>>(users);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error in UserRepository GetAllAsync: {ex.Message}";
        }

        return response;
    }
}
