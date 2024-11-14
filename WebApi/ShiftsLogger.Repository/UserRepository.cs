using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            var users = await _context.Users
                .Where(u => u.IsActive == true)
                .ProjectTo<User>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (users.Count == 0)
            {
                response.Message = "No users found in the database!";
                response.Success = false;
            }
            else
            {
                response.Data = users;
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository {nameof(GetAllAsync)}: {ex.Message}";
            response.Success = false;
        }

        return response;
    }

    public async Task<ApiResponse<User>> GetByIdAsync(Guid id)
    {
        var response = new ApiResponse<User>();

        try
        {
            var user = await _context.Users
                .Where(u => u.IsActive == true)
                .ProjectTo<User>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                response.Message = $"User with Id {id} doesn't exist";
                response.Success = false;
            }
            else
            {
                response.Data = user;
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository {nameof(GetByIdAsync)}: {ex.Message}";
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
            response.Message = $"Error in UserRepository {nameof(CreateAsync)}: {ex.Message}";
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
            response.Message = $"Error in UserRepository {nameof(DeleteAsync)}: {ex.Message}";
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
            response.Message = $"Error in UserRepository {nameof(UpdateAsync)}: {ex.Message}";
            response.Success = false;
        }
        return response;
    }

    public async Task<ApiResponse<List<User>>> CreateManyAsync(List<User> users)
    {
        var response = new ApiResponse<List<User>>();

        try
        {
            var userEntities = _mapper.Map<List<UserEntity>>(users);
            _context.AddRange(userEntities);
            await _context.SaveChangesAsync();

            response.Data = users;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository {nameof(CreateManyAsync)}: {ex.Message}";
            response.Success = false;
        }
        return response;
    }

    public async Task<ApiResponse<User>> UpdateShiftsAsync(Guid id, List<Shift> shifts)
    {
        var response = new ApiResponse<User>();
        try
        {
            var userEntity = _context.Users
                .Include(u => u.Shifts.Where(s => s.IsActive == true))
                .Where(u => u.IsActive == true)
                .SingleOrDefault(u => u.Id == id);

            if (userEntity is null)
            {
                response.Message = $"User with Id {id} doesn't exist";
                response.Success = false;
                return response;
            }

            var shiftsToRemove = userEntity.Shifts
                .Where(se => !shifts.Any(s => s.Id == se.Id))
                .ToList();

            var shiftsToAdd = shifts
                .Where(s => !userEntity.Shifts.Any(se => se.Id == s.Id))
                .ToList();

            if (shiftsToRemove.Count != 0)
            {
                foreach (var shift in shiftsToRemove)
                {
                    userEntity.Shifts.Remove(shift);
                }
            }
            if (shiftsToAdd.Count != 0)
            {
                var shiftEntitiesToAdd = _mapper.Map<List<ShiftEntity>>(shiftsToAdd);
                _context.Shifts.AttachRange(shiftEntitiesToAdd);
                userEntity.Shifts.AddRange(shiftEntitiesToAdd);
            }

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<User>(userEntity);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository {nameof(UpdateShiftsAsync)}: {ex.Message}";
            response.Success = false;
        }
        return response;
    }

    public async Task<ApiResponse<List<Shift>>> GetShiftsByUserIdAsync(Guid id)
    {
        var response = new ApiResponse<List<Shift>>();
        try
        {
            var shifts = await _context.Users
                .Where(u => u.IsActive == true && u.Id == id)
                .SelectMany(u => u.Shifts.Where(s => s.IsActive == true))
                .ProjectTo<Shift>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (shifts.Count == 0)
            {
                response.Message = $"No shifts found!";
                response.Success = false;
            }
            else
            {
                response.Data = shifts;
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in UserRepository {nameof(GetShiftsByUserIdAsync)}: {ex.Message}";
            response.Success = false;
        }
        return response;
    }
}
