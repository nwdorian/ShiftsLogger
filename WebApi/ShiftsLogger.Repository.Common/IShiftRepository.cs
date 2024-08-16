using ShiftsLogger.Model;

namespace ShiftsLogger.Repository.Common;
public interface IShiftRepository
{
    Task<ApiResponse<List<Shift>>> GetAllAsync();
    Task<ApiResponse<Shift>> GetByIdAsync(Guid id);
    Task<ApiResponse<Shift>> CreateAsync(Shift shift);
    Task<ApiResponse<Shift>> DeleteAsync(Shift shift);
    Task<ApiResponse<Shift>> UpdateAsync(Shift shift);
}
