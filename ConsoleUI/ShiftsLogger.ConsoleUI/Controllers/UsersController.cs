using ShiftsLogger.ConsoleUI.Models;
using ShiftsLogger.ConsoleUI.Services;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Controllers;
public class UsersController
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task GetAllUsers()
    {
        var users = await _usersService.GetAll();
        if (users.Count == 0)
        {
            return;
        }

        TableVisualization.DisplayUsersTable(users);
        var user = GetUserFromList(users);
        if (user is null)
        {
            return;
        }

        var shifts = await _usersService.GetShiftsByUserId(user.Id);
        if (shifts.Count == 0)
        {
            return;
        }

        TableVisualization.DisplayUserDetailsTable(user, shifts);
        UserInput.PromptAnyKeyToContinue();
    }

    public async Task AddUser()
    {
        var user = CreateNewUser();
        TableVisualization.DisplayUserTable(user);
        if (!AnsiConsole.Confirm("Are you sure you want to create a new user?"))
        {
            return;
        }

        if (await _usersService.CreateUser(user))
        {
            AnsiConsole.MarkupLine("New user created successfully!");
            UserInput.PromptAnyKeyToContinue();
        }
    }

    public async Task DeleteUser()
    {
        var users = await _usersService.GetAll();
        if (users.Count == 0)
        {
            return;
        }

        TableVisualization.DisplayUsersTable(users);
        var user = GetUserFromList(users);
        if (user is null)
        {
            return;
        }

        AnsiConsole.Clear();
        TableVisualization.DisplayUserTable(user);
        if (!AnsiConsole.Confirm("Are you sure you want delete this user?"))
        {
            return;
        }

        if (await _usersService.DeleteUser(user.Id))
        {
            AnsiConsole.MarkupLine($"{user.ToString()} deleted successfully!");
            UserInput.PromptAnyKeyToContinue();
        }
    }

    public async Task UpdateUser()
    {
        var users = await _usersService.GetAll();
        if (users.Count == 0)
        {
            return;
        }

        TableVisualization.DisplayUsersTable(users);
        var user = GetUserFromList(users);
        if (user is null)
        {
            return;
        }

        AnsiConsole.Clear();
        TableVisualization.DisplayUserTable(user);
        var userToUpdate = CreateUserToUpdate();
        if (!AnsiConsole.Confirm("Are you sure you want update this user?"))
        {
            return;
        }

        if (await _usersService.UpdateUser(user.Id, userToUpdate))
        {
            AnsiConsole.MarkupLine($"User updated successfully!");
            UserInput.PromptAnyKeyToContinue();
        }
    }

    private User? GetUserFromList(List<User> users)
    {
        var input = UserInput.PromptNumberInput("Enter user ID (or press 0 to exit):");
        var index = Validation.ValidateListIndex(input, users);
        return users.ElementAtOrDefault(index);
    }
    private UserCreate CreateNewUser()
    {
        AnsiConsole.MarkupLine("Creating a new user. Enter information:");
        var firstName = UserInput.PromptStringInput("First name:");
        var lastName = UserInput.PromptStringInput("Last name:");
        var email = UserInput.PromptStringInput("Email:");
        email = Validation.ValidateEmailInput(email);

        return new UserCreate(firstName, lastName, email);
    }

    private UserUpdate CreateUserToUpdate()
    {
        AnsiConsole.MarkupLine("Enter new information (or leave empty):");
        var firstName = UserInput.PromptStringAllowEmptyInput("First name:");
        var lastName = UserInput.PromptStringAllowEmptyInput("Last name:");
        var email = UserInput.PromptStringAllowEmptyInput("Email:");
        if (!String.IsNullOrWhiteSpace(email))
        {
            email = Validation.ValidateEmailInput(email);
        }
        return new UserUpdate(firstName, lastName, email);
    }

}
