using ShiftsLogger.ConsoleUI.Services;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Menus;
public class UsersMenu
{
    private readonly UserService _userService;

    public UsersMenu(UserService userService)
    {
        _userService = userService;
    }
    public async Task DisplayAsync()
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<UsersMenuOptions>()
                .Title("Select from the menu:")
                .AddChoices(Enum.GetValues<UsersMenuOptions>()));

            switch (selection)
            {
                case UsersMenuOptions.ViewAllUsers:
                    await _userService.GetAll();
                    break;
                case UsersMenuOptions.AddUser:
                    break;
                case UsersMenuOptions.DeleteUser:
                    break;
                case UsersMenuOptions.UpdateUser:
                    break;
                case UsersMenuOptions.EditShifts:
                    break;
                case UsersMenuOptions.MainMenu:
                    exit = true;
                    break;
            }
        }
    }

    private enum UsersMenuOptions
    {
        ViewAllUsers,
        AddUser,
        DeleteUser,
        UpdateUser,
        EditShifts,
        MainMenu
    }
}
