using ShiftsLogger.ConsoleUI.Models;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI;
public static class Helpers
{
    public static User? GetUserFromList(List<User> users)
    {
        var input = UserInput.PromptPositiveInteger("Enter user ID (or press 0 to exit):", AllowZero.True);
        var index = UserInput.GetValidListIndex(input, users);
        return users.ElementAtOrDefault(index);
    }

    public static UserCreate CreateNewUser()
    {
        AnsiConsole.MarkupLine("Creating a new user. Enter information:");
        var firstName = UserInput.PromptString("First name:", AllowEmpty.False);
        var lastName = UserInput.PromptString("Last name:", AllowEmpty.False);
        var email = UserInput.PromptString("Email:", AllowEmpty.False, Validate.Email);

        return new UserCreate(firstName, lastName, email);
    }

    public static UserUpdate CreateUserToUpdate()
    {
        AnsiConsole.MarkupLine("Enter new information (or leave empty):");
        var firstName = UserInput.PromptString("First name:", AllowEmpty.True);
        var lastName = UserInput.PromptString("Last name:", AllowEmpty.True);
        var email = UserInput.PromptString("Email:", AllowEmpty.True, Validate.Email);
        return new UserUpdate(firstName, lastName, email);
    }

    public static Shift? GetShiftFromList(List<Shift> shifts)
    {
        var input = UserInput.PromptPositiveInteger("Enter shift ID (or press 0 to exit):", AllowZero.True);
        var index = UserInput.GetValidListIndex(input, shifts);
        return shifts.ElementAtOrDefault(index);
    }

    public static ShiftCreate CreateNewShift()
    {
        AnsiConsole.MarkupLine("Creating a new shift. Enter information:");
        AnsiConsole.MarkupLine("Format: [blue]YYYY-MM-DD HH:MM[/]");
        var start = UserInput.PromptDateTime("Start date and time:", AllowEmpty.False);
        var duration = UserInput.PromptPositiveInteger("Duration in hours:", AllowZero.False);
        var end = start.AddHours(duration);
        return new ShiftCreate(start, end);
    }

    public static ShiftUpdate CreateShiftToUpdate(Shift shift)
    {
        AnsiConsole.MarkupLine("Enter new information:");
        AnsiConsole.MarkupLine("Format: [blue]YYYY-MM-DD HH:MM[/]");
        var start = UserInput.PromptDateTime("Start date and time (leave empty to skip):", AllowEmpty.True);
        var duration = UserInput.PromptPositiveInteger("Duration in hours (enter 0 to skip):", AllowZero.True);

        return new ShiftUpdate
            (
            start,
            duration == 0 ? DateTime.MinValue : start.AddHours(shift.GetDuration())
            );
    }
}
