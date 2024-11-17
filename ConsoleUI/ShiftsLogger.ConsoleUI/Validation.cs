using Spectre.Console;
using System.Net.Mail;

namespace ShiftsLogger.ConsoleUI;
public static class Validation
{
    public static int IsValidListIndex<T>(int input, List<T> list) where T : class
    {
        while (input > list.Count)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input![/]");
            input = UserInput.PromptPositiveIntegerAllowZero("Select a valid Id (or press 0 to exit):");
        }
        return input - 1;
    }

    public static string IsValidEmailInput(string input)
    {
        while (!IsValidEmail(input))
        {
            AnsiConsole.MarkupLine($"[red]Invalid input![/]");
            input = UserInput.PromptString("Enter a valid email adress:");
        }
        return input;
    }

    public static bool IsValidEmail(string input)
    {
        try
        {
            var email = new MailAddress(input);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static ValidationResult IsValidDateTime(DateTime input)
    {
        try
        {
            var dateOnly = new DateOnly(input.Year, input.Month, input.Day);
            var timeOnly = new TimeOnly(input.Hour, input.Minute);
            if (timeOnly.Hour == 0 && timeOnly.Minute == 0)
            {
                return ValidationResult.Error("[red]Start time was not set![/]");
            }
            var date = new DateTime(dateOnly, timeOnly);
            return ValidationResult.Success();
        }
        catch (Exception)
        {
            return ValidationResult.Error("[red]Invalid date time input![/]");
        }
    }

    public static ValidationResult IsGreaterOrEqualToZero(int input)
    {
        return input switch
        {
            < 0 => ValidationResult.Error("[red]Input must be greater than or equal to 0![/]"),
            _ => ValidationResult.Success()
        };
    }

    public static ValidationResult IsGreaterThanZero(int input)
    {
        return input switch
        {
            < 1 => ValidationResult.Error("[red]Input must be greater than 0![/]"),
            _ => ValidationResult.Success()
        };
    }
}
