using Refit;
using ShiftsLogger.ConsoleUI.Models;

namespace ShiftsLogger.ConsoleUI.RefitClients;
public interface IUsersClient
{
    [Get("/user")]
    Task<ApiResponse<List<User>>> GetAll();
}
