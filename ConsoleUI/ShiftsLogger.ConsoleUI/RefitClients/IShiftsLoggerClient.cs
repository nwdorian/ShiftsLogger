using Refit;
using ShiftsLogger.ConsoleUI.Models;

namespace ShiftsLogger.ConsoleUI.RefitClients;
public interface IShiftsLoggerClient
{
    [Post("/seeding")]
    Task<ApiResponse<string>> SeedDatabase();

    [Get("/users")]
    Task<ApiResponse<List<User>>> GetAll();

    [Get("/users/{id}/shifts")]
    Task<ApiResponse<List<Shift>>> GetShiftsByUserId(Guid id);

    [Post("/users")]
    Task<ApiResponse<User>> CreateUser(UserCreate user);

    [Delete("/users/{id}")]
    Task<IApiResponse> DeleteUser(Guid id);

    [Put("/users/{id}")]
    Task<IApiResponse> UpdateUser(Guid id, [Body] UserUpdate user);
}
