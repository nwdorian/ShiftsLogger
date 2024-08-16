using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.DAL;
using ShiftsLogger.DAL.Entities;
using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;

namespace ShiftsLogger.Repository;
public class ShiftRepository : IShiftRepository
{
    private readonly ShiftsContext _context;
    private readonly IMapper _mapper;

    public ShiftRepository(ShiftsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<Shift>>> GetAllAsync()
    {
        var response = new ApiResponse<List<Shift>>();
        try
        {
            var shifts = await _context.Shifts.Where(s => s.IsActive == true).ToListAsync();

            if (shifts.Count == 0)
            {
                response.Message = "No shifts found in the database!";
                response.Success = false;
            }
            else
            {
                response.Data = _mapper.Map<List<Shift>>(shifts);
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in ShiftRepository GetAllAsync: {ex.Message}";
            response.Success = false;
        }
        return response;

    }

    public async Task<ApiResponse<Shift>> GetByIdAsync(Guid id)
    {
        var response = new ApiResponse<Shift>();
        try
        {
            var shift = await _context.Shifts.Where(s => s.IsActive == true).SingleOrDefaultAsync(s => s.Id == id);

            if (shift is null)
            {
                response.Message = $"Shift with Id {id} doesn't exist";
                response.Success = false;
            }
            else
            {
                response.Data = _mapper.Map<Shift>(shift);
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in ShiftRepository GetByIdAsync: {ex.Message}";
            response.Success = false;
        }
        return response;
    }
    public async Task<ApiResponse<Shift>> CreateAsync(Shift shift)
    {
        var response = new ApiResponse<Shift>();
        try
        {
            var shiftEntity = _mapper.Map<UserEntity>(shift);
            _context.Add(shiftEntity);
            await _context.SaveChangesAsync();

            response.Data = shift;
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in ShiftRepository CreateAsync: {ex.Message}";
            response.Success = false;
        }
        return response;
    }

    public async Task<ApiResponse<Shift>> DeleteAsync(Shift shift)
    {
        var response = new ApiResponse<Shift>();
        try
        {
            var shiftEntity = _mapper.Map<UserEntity>(shift);
            _context.Update(shiftEntity);
            await _context.SaveChangesAsync();

            response.Message = "Succesfully deleted!";
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in ShiftRepository DeleteAsync: {ex.Message}";
            response.Success = false;
        }
        return response;
    }


    public async Task<ApiResponse<Shift>> UpdateAsync(Shift shift)
    {
        var response = new ApiResponse<Shift>();
        try
        {
            var shiftEntity = _mapper.Map<UserEntity>(shift);
            _context.Update(shiftEntity);
            await _context.SaveChangesAsync();

            response.Message = "Successfully updated!";
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in ShiftRepository UpdateAsync: {ex.Message}";
            response.Success = false;
        }
        return response;
    }
}
