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

        await _usersService.CreateUser(user);
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

        TableVisualization.DisplayUserTable(user);
        if (!AnsiConsole.Confirm("Are you sure you want delete this user?"))
        {
            return;
        }

        await _usersService.DeleteUser(user.Id);

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

        TableVisualization.DisplayUserTable(user);
        var userToUpdate = CreateUserToUpdate();
        if (!AnsiConsole.Confirm("Are you sure you want update this user?"))
        {
            return;
        }

        await _usersService.UpdateUser(user.Id, userToUpdate);
    }

    private User? GetUserFromList(List<User> users)
    {
        var input = UserInput.PromptPositiveIntegerAllowZero("Enter user ID (or press 0 to exit):");
        var index = Validation.IsValidListIndex(input, users);
        return users.ElementAtOrDefault(index);
    }
    private UserCreate CreateNewUser()
    {
        AnsiConsole.MarkupLine("Creating a new user. Enter information:");
        var firstName = UserInput.PromptString("First name:");
        var lastName = UserInput.PromptString("Last name:");
        var email = UserInput.PromptString("Email:");
        email = Validation.IsValidEmailInput(email);

        return new UserCreate(firstName, lastName, email);
    }

    private UserUpdate CreateUserToUpdate()
    {
        AnsiConsole.MarkupLine("Enter new information (or leave empty):");
        var firstName = UserInput.PromptStringAllowEmpty("First name:");
        var lastName = UserInput.PromptStringAllowEmpty("Last name:");
        var email = UserInput.PromptStringAllowEmpty("Email:");
        if (!String.IsNullOrWhiteSpace(email))
        {
            email = Validation.IsValidEmailInput(email);
        }
        return new UserUpdate(firstName, lastName, email);
    }

}
