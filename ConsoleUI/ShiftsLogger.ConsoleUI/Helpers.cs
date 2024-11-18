using ShiftsLogger.ConsoleUI.Models;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class Helpers
{
    public static User? GetUserFromList(List<User> users)
    {
        var input = UserInput.PromptPositiveInteger("Enter user ID (or press 0 to exit):");
        var index = UserInput.CheckValidListIndex(input, users);
        return users.ElementAtOrDefault(index);
    }

    public static UserCreate CreateNewUser()
    {
        AnsiConsole.MarkupLine("Creating a new user. Enter information:");
        var firstName = UserInput.PromptString("First name:");
        var lastName = UserInput.PromptString("Last name:");
        var email = UserInput.PromptString("Email:");
        email = UserInput.CheckValidEmailInput(email);

        return new UserCreate(firstName, lastName, email);
    }

    public static UserUpdate CreateUserToUpdate()
    {
        AnsiConsole.MarkupLine("Enter new information (or leave empty):");
        var firstName = UserInput.PromptStringAllowEmpty("First name:");
        var lastName = UserInput.PromptStringAllowEmpty("Last name:");
        var email = UserInput.PromptStringAllowEmpty("Email:");
        if (!String.IsNullOrWhiteSpace(email))
        {
            email = UserInput.CheckValidEmailInput(email);
        }
        return new UserUpdate(firstName, lastName, email);
    }

    public static Shift? GetShiftFromList(List<Shift> shifts)
    {
        var input = UserInput.PromptPositiveInteger("Enter shift ID (or press 0 to exit):");
        var index = UserInput.CheckValidListIndex(input, shifts);
        return shifts.ElementAtOrDefault(index);
    }

    public static ShiftCreate CreateNewShift()
    {
        AnsiConsole.MarkupLine("Creating a new shift. Enter information:");
        AnsiConsole.MarkupLine("Format: [blue]YYYY-MM-DD HH:MM[/]");
        var startDateTime = UserInput.PromptDateTime("Start date and time:");
        var duration = UserInput.PromptPositiveInteger("Duration in hours:", allowZero: false);
        var endDateTime = startDateTime.AddHours(duration);
        return new ShiftCreate(startDateTime, endDateTime);
    }

    public static ShiftUpdate CreateShiftToUpdate(Shift shift)
    {
        AnsiConsole.MarkupLine("Enter new information:");
        AnsiConsole.MarkupLine("Format: [blue]YYYY-MM-DD HH:MM[/]");
        var startDateTime = UserInput.PromptDateTimeAllowEmpty("Start date and time (leave empty to skip):");
        if (startDateTime == DateTime.MinValue)
        {
            startDateTime = shift.StartTime;
        }
        var duration = UserInput.PromptPositiveInteger("Duration in hours:", allowZero: false);
        var endDateTime = startDateTime.AddHours(duration);
        return new ShiftUpdate(startDateTime, endDateTime);
    }
}
