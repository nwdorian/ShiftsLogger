using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class UserInput
{
    public static void PromptAnyKeyToContinue()
    {
        AnsiConsole.Write("Press any key to continue...");
        Console.ReadLine();
    }
    public static int PromptPositiveIntegerAllowZero(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>(prompt)
            .ValidationErrorMessage("[red]Input must be an integer![/]")
                .Validate(Validation.IsGreaterOrEqualToZero));
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

    public static int PromptPositiveInteger(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>(prompt)
            .ValidationErrorMessage("[red]Input must be an integer![/]")
            .Validate(Validation.IsGreaterThanZero)
            );
    }
}
