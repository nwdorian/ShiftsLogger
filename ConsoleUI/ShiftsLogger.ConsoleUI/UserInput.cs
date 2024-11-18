using ShiftsLogger.ConsoleUI.Models;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class UserInput
{
    public static void PromptAnyKeyToContinue()
    {
        AnsiConsole.Write("Press any key to continue...");
        Console.ReadLine();
    }

    public static int PromptPositiveInteger(string prompt, bool allowZero = true)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>(prompt)
            .ValidationErrorMessage("[red]Input must be an integer![/]")
            .Validate(allowZero ? Validation.IsGreaterOrEqualToZero : Validation.IsGreaterThanZero)
            );
    }

    public static string PromptString(string prompt)
    {
        return AnsiConsole.Ask<string>(prompt);
    }

    public static string PromptStringAllowEmpty(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .AllowEmpty());
    }

    public static DateTime PromptDateTime(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<DateTime>(prompt)
            .ValidationErrorMessage("[red]Invalid format![/]")
            .Validate(Validation.IsValidDateTime));
    }

    public static DateTime PromptDateTimeAllowEmpty(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<DateTime>(prompt)
            .ValidationErrorMessage("[red]Invalid format![/]")
            .AllowEmpty());
    }

    public static int CheckValidListIndex<T>(int input, List<T> list) where T : class
    {
        while (input > list.Count)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input![/]");
            input = UserInput.PromptPositiveInteger("Select a valid Id (or press 0 to exit):");
        }
        return input - 1;
    }

    public static string CheckValidEmailInput(string input)
    {
        while (!Validation.IsValidEmail(input))
        {
            AnsiConsole.MarkupLine($"[red]Invalid input![/]");
            input = UserInput.PromptString("Enter a valid email adress:");
        }
        return input;
    }

    public static List<Shift> GetUserShifts(List<Shift> allShifts, List<Shift> userShifts)
    {
        var multiSelection = new MultiSelectionPrompt<Shift>();
        multiSelection.Title("Select shifts: ")
            .NotRequired()
            .InstructionsText(
            "[red]Note[/]: Shifts already associated with a user are pre-selected!\n" +
            "[grey](Press [blue]<space>[/] to toggle a shift,[green]<enter>[/] to accept)[/]"
            )
            .AddChoices(allShifts)
            .UseConverter(s => $"{s.StartTime.ToShortDateString()} {s.StartTime.ToShortTimeString()}-{s.EndTime.ToShortTimeString()}");


        foreach (var shift in allShifts)
        {
            if (userShifts.Any(s => s.Id == shift.Id))
            {
                multiSelection.Select(shift);
            }
        }

        AnsiConsole.Clear();
        return AnsiConsole.Prompt(multiSelection);
    }
}
