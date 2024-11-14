using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class UserInput
{
    public static int GetIndexInput(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>(prompt)
            .ValidationErrorMessage("[red]Input must be an integer![/]")
                .Validate(n => n switch
                {
                    < 0 => ValidationResult.Error("[red]Id must be greater than or equal to 0![/]"),
                    _ => ValidationResult.Success()
                }));
    }
}
