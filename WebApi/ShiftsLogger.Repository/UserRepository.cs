using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL;
using ShiftsLogger.DAL.Entities;
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
            response.Message = $"Error in UserRepository GetByIdAsync: {ex.Message}";
            response.Success = false;
        }
        return response;
    }

    public async Task<ApiResponse<User>> CreateAsync(User user)
    {
        var response = new ApiResponse<User>();
        try
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            _context.Add(userEntity);
            await _context.SaveChangesAsync();

            response.Data = user;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository CreateAsync: {ex.Message}";
            response.Success = false;
        }
        return response;
    }

    public async Task<ApiResponse<User>> DeleteAsync(User user)
    {
        var response = new ApiResponse<User>();
        try
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            _context.Update(userEntity);
            await _context.SaveChangesAsync();

            response.Message = "Succesfully deleted!";
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository DeleteAsync: {ex.Message}";
            response.Success = false;
        }
        return response;
    }

    public async Task<ApiResponse<User>> UpdateAsync(User user)
    {
        var response = new ApiResponse<User>();

        try
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            _context.Update(userEntity);
            await _context.SaveChangesAsync();

            response.Message = "Successfully updated!";
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository DeleteAsync: {ex.Message}";
            response.Success = false;
        }
        return response;
    }
}
