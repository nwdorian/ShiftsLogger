using ShiftsLogger.Model;
using ShiftsLogger.Repository.Common;
using ShiftsLogger.Service.Common;

namespace ShiftsLogger.Service;
public class ShiftService : IShiftService
{
    private readonly IShiftRepository _shiftRepository;

    public ShiftService(IShiftRepository shiftRepository)
    {
        _shiftRepository = shiftRepository;
    }
    public async Task<ApiResponse<List<Shift>>> GetAllAsync()
    {
        return await _shiftRepository.GetAllAsync();
    }

    public async Task<ApiResponse<Shift>> GetByIdAsync(Guid id)
    {
        return await _shiftRepository.GetByIdAsync(id);

    }

    public async Task<ApiResponse<Shift>> CreateAsync(Shift shift)
    {
        shift.Id = Guid.NewGuid();
        shift.IsActive = true;
        shift.DateCreated = DateTime.Now;
        shift.DateUpdated = DateTime.Now;

        return await _shiftRepository.CreateAsync(shift);
    }

    public async Task<ApiResponse<Shift>> DeleteAsync(Guid id)
    {
        var response = await _shiftRepository.GetByIdAsync(id);

        if (response.Success == false)
        {
            response.Message = "Shift not found";
        }

        var shift = response.Data;

        if (shift is null)
        {
            response.Message = "Shift is null";
            return response;
        }
        shift.IsActive = false;

        return await _shiftRepository.DeleteAsync(shift);
    }


    public async Task<ApiResponse<Shift>> UpdateAsync(Guid id, Shift shift)
    {
        var response = await _shiftRepository.GetByIdAsync(id);

        if (response.Success == false)
        {
            response.Message = "Shift not found";
            return response;
        }

        var existingShift = response.Data;

        if (existingShift is null)
        {
            response.Message = "Shift is null";
            return response;
        }

        if (shift.StartTime != DateTime.MinValue)
        {
            existingShift.StartTime = shift.StartTime;
        }

        if (shift.EndTime != DateTime.MinValue)
        {
            existingShift.EndTime = shift.EndTime;
        }

        existingShift.DateUpdated = DateTime.Now;

        return await _shiftRepository.UpdateAsync(existingShift);
    }
}
