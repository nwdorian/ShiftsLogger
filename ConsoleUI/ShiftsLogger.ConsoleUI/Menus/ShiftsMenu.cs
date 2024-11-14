using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Menus;
public class ShiftsMenu
{
    public async Task DisplayAsync()
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<ShiftsMenuOptions>()
                .Title("Select from the menu:")
                .AddChoices(Enum.GetValues<ShiftsMenuOptions>()));

            switch (selection)
            {
                case ShiftsMenuOptions.ViewAllShifts:
                    break;
                case ShiftsMenuOptions.AddShift:
                    break;
                case ShiftsMenuOptions.DeleteShift:
                    break;
                case ShiftsMenuOptions.UpdateShift:
                    break;
                case ShiftsMenuOptions.MainMenu:
                    exit = true;
                    break;
            }
        }
    }

    private enum ShiftsMenuOptions
    {
        ViewAllShifts,
        AddShift,
        DeleteShift,
        UpdateShift,
        MainMenu
    }
}
