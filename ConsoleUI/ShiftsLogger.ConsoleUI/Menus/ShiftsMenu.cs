using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Menus;
public class ShiftsMenu : BaseMenu
{
    public override async Task DisplayAsync()
    {
        var exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<Options>()
                .Title("Select from the menu:")
                .AddChoices(Enum.GetValues<Options>()));

            switch (selection)
            {
                case Options.ViewAllShifts:
                    break;
                case Options.AddShift:
                    break;
                case Options.DeleteShift:
                    break;
                case Options.UpdateShift:
                    break;
                case Options.MainMenu:
                    exit = true;
                    break;
            }
        }
    }

    private enum Options
    {
        ViewAllShifts,
        AddShift,
        DeleteShift,
        UpdateShift,
        MainMenu
    }
}
