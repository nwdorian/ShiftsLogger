using Spectre.Console;
using System.Net.Mail;

namespace ShiftsLogger.ConsoleUI;
public static class Validation
{
    public static bool IsValidEmail(string input)
    {
        try
        {
            var email = new MailAddress(input);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static ValidationResult IsValidDateTime(DateTime input)
    {

        var timeOnly = new TimeOnly(input.Hour, input.Minute);
        if (timeOnly.Hour == 0 && timeOnly.Minute == 0)
        {
            return ValidationResult.Error("[red]Start time was not set![/]");
        }
        return ValidationResult.Success();
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
