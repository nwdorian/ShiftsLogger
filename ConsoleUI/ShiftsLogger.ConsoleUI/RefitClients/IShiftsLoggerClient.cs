using Refit;
using ShiftsLogger.ConsoleUI.Models;

namespace ShiftsLogger.ConsoleUI.RefitClients;
public interface IShiftsLoggerClient
{
    [Post("/seeding")]
    Task<ApiResponse<string>> SeedDatabase();

    [Get("/users")]
    Task<ApiResponse<List<User>>> GetAllUsers();

    [Get("/users/{id}/shifts")]
    Task<ApiResponse<List<Shift>>> GetShiftsByUserId(Guid id);

    [Post("/users")]
    Task<ApiResponse<User>> CreateUser([Body] UserCreate user);

    [Delete("/users/{id}")]
    Task<IApiResponse> DeleteUser(Guid id);

    [Put("/users/{id}")]
    Task<IApiResponse> UpdateUser(Guid id, [Body] UserUpdate user);

    [Get("/shifts")]
    Task<ApiResponse<List<Shift>>> GetAllShifts();

    [Post("/shifts")]
    Task<ApiResponse<Shift>> CreateShift([Body] ShiftCreate shift);

    [Delete("/shifts/{id}")]
    Task<IApiResponse> DeleteShift(Guid id);

    [Put("/shifts/{id}")]
    Task<IApiResponse> UpdateShift(Guid id, [Body] ShiftUpdate shift);

    [Put("/users/{id}/shifts")]
    Task<IApiResponse> UpdateUserShifts(Guid id, [Body] List<Shift> shifts);
}
