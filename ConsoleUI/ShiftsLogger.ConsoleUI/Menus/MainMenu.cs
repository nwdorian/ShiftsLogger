using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Menus;
public class MainMenu
{
    private readonly UsersMenu _usersMenu;
    private readonly ShiftsMenu _shiftsMenu;

    public MainMenu(UsersMenu usersMenu, ShiftsMenu shiftsMenu)
    {
        _usersMenu = usersMenu;
        _shiftsMenu = shiftsMenu;
    }
    public async Task DisplayAsync()
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new FigletText("Shifts Logger")
                    .LeftJustified()
                    .Color(Color.Yellow));

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("Select from the menu:")
                .AddChoices(Enum.GetValues<MainMenuOptions>()));

            switch (selection)
            {
                case MainMenuOptions.ManageUsers:
                    await _usersMenu.DisplayAsync();
                    break;
                case MainMenuOptions.ManageShifts:
                    await _shiftsMenu.DisplayAsync();
                    break;
                case MainMenuOptions.Exit:
                    if (AnsiConsole.Confirm("Are you sure you want to exit?"))
                    {
                        Console.WriteLine("Goodbye!");
                        exit = true;
                    }
                    else
                    {
                        exit = false;
                    }
                    break;
            }
        }
    }
    private enum MainMenuOptions
    {
        ManageUsers,
        ManageShifts,
        Exit
    }
}
