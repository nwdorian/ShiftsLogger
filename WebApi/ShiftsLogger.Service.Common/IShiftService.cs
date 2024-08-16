using ShiftsLogger.Model;

namespace ShiftsLogger.Service.Common;
public interface IShiftService
{
    Task<ApiResponse<List<Shift>>> GetAllAsync();
    Task<ApiResponse<Shift>> GetByIdAsync(Guid id);
    Task<ApiResponse<Shift>> CreateAsync(Shift shift);
    Task<ApiResponse<Shift>> DeleteAsync(Guid id);
    Task<ApiResponse<Shift>> UpdateAsync(Guid id, Shift shift);
}
