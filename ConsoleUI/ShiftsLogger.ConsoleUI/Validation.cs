using Spectre.Console;
using System.Net.Mail;

namespace ShiftsLogger.ConsoleUI;
public static class Validation
{
    public static int ValidateListIndex<T>(int input, List<T> list) where T : class
    {
        while (input > list.Count)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input![/]");
            input = UserInput.PromptNumberInput("Select a valid Id (or press 0 to exit):");
        }
        return input - 1;
    }

    public static string ValidateEmailInput(string input)
    {
        while (!IsValidEmail(input))
        {
            AnsiConsole.MarkupLine($"[red]Invalid input![/]");
            input = UserInput.PromptStringInput("Enter a valid email adress:");
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
}
