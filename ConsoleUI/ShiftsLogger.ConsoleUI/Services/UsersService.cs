﻿using ShiftsLogger.ConsoleUI.Models;
using ShiftsLogger.ConsoleUI.RefitClients;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Services;
public class UsersService
{
    private readonly IShiftsLoggerClient _apiClient;

    public UsersService(IShiftsLoggerClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<List<User>> GetAll()
    {
        var users = new List<User>();
        try
        {
            var response = await _apiClient.GetAllUsers();

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
            var response = await _apiClient.GetShiftsByUserId(id);

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

    public async Task CreateUser(UserCreate user)
    {
        try
        {
            var response = await _apiClient.CreateUser(user);

            if (response.IsSuccessful)
            {
                AnsiConsole.MarkupLine("New user created successfully!");
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

    public async Task DeleteUser(Guid id)
    {
        try
        {
            var response = await _apiClient.DeleteUser(id);

            if (response.IsSuccessful)
            {
                AnsiConsole.MarkupLine($"User deleted successfully!");
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

    public async Task UpdateUser(Guid id, UserUpdate user)
    {
        try
        {
            var response = await _apiClient.UpdateUser(id, user);

            if (response.IsSuccessful)
            {
                AnsiConsole.MarkupLine($"User updated successfully!");
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
