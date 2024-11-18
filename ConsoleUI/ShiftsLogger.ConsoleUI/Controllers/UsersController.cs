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
        var user = Helpers.GetUserFromList(users);
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
        var user = Helpers.CreateNewUser();
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
        var user = Helpers.GetUserFromList(users);
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
        var user = Helpers.GetUserFromList(users);
        if (user is null)
        {
            return;
        }

        TableVisualization.DisplayUserTable(user);
        var userToUpdate = Helpers.CreateUserToUpdate();
        if (!AnsiConsole.Confirm("Are you sure you want update this user?"))
        {
            return;
        }

        await _usersService.UpdateUser(user.Id, userToUpdate);
    }
}
