using ShiftsLogger.ConsoleUI.Models;
using ShiftsLogger.ConsoleUI.RefitClients;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Services;
public class ShiftsService
{
    private readonly IShiftsLoggerClient _apiClient;

    public ShiftsService(IShiftsLoggerClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<Shift>> GetAll()
    {
        var shifts = new List<Shift>();
        try
        {
            var response = await _apiClient.GetAllShifts();

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

    public async Task CreateShift(ShiftCreate shift)
    {
        try
        {
            var response = await _apiClient.CreateShift(shift);

            if (response.IsSuccessful)
            {
                AnsiConsole.MarkupLine("New shift created successfully!");
                UserInput.PromptAnyKeyToContinue();
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
    }

    public async Task DeleteShift(Guid id)
    {
        try
        {
            var response = await _apiClient.DeleteShift(id);

            if (response.IsSuccessful)
            {
                AnsiConsole.MarkupLine($"Shift deleted successfully!");
                UserInput.PromptAnyKeyToContinue();
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
    }

    public async Task UpdateShift(Guid id, ShiftUpdate shift)
    {
        try
        {
            var response = await _apiClient.UpdateShift(id, shift);

            if (response.IsSuccessful)
            {
                AnsiConsole.MarkupLine($"Shift updated successfully!");
                UserInput.PromptAnyKeyToContinue();
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
    }
}
