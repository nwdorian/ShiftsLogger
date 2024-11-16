using ShiftsLogger.ConsoleUI.Models;
using ShiftsLogger.ConsoleUI.RefitClients;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Services;
public class UsersService
{
    private readonly IShiftsLoggerClient _userClient;

    public UsersService(IShiftsLoggerClient userClient)
    {
        _userClient = userClient;
    }
    public async Task<List<User>> GetAll()
    {
        var users = new List<User>();
        try
        {
            var response = await _userClient.GetAll();

            if (response.IsSuccessful)
            {
                users = response.Content;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.Error.Content ?? response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
            Console.ReadLine();
        }
        return users;
    }

    public async Task<List<Shift>> GetShiftsByUserId(Guid id)
    {
        var shifts = new List<Shift>();
        try
        {
            var response = await _userClient.GetShiftsByUserId(id);

            if (response.IsSuccessful)
            {
                shifts = response.Content;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.Error.Content ?? response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
            Console.ReadLine();
        }
        return shifts;
    }

    public async Task<bool> CreateUser(UserCreate user)
    {
        var isUserCreated = false;
        try
        {
            var response = await _userClient.CreateUser(user);

            if (response.IsSuccessful)
            {
                isUserCreated = true;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.Error.Content ?? response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
            Console.ReadLine();
        }
        return isUserCreated;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var isUserDeleted = false;
        try
        {
            var response = await _userClient.DeleteUser(id);

            if (response.IsSuccessful)
            {
                isUserDeleted = true;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.Error.Content ?? response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
            Console.ReadLine();
        }
        return isUserDeleted;
    }

    public async Task<bool> UpdateUser(Guid id, UserUpdate user)
    {
        var isUserUpdated = false;
        try
        {
            var response = await _userClient.UpdateUser(id, user);

            if (response.IsSuccessful)
            {
                isUserUpdated = true;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.Error.Content ?? response.Error.Message}[/]");
                UserInput.PromptAnyKeyToContinue();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
            Console.ReadLine();
        }
        return isUserUpdated;
    }
}
