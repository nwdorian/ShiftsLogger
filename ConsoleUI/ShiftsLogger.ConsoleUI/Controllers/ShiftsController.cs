using ShiftsLogger.ConsoleUI.Models;
using ShiftsLogger.ConsoleUI.Services;
using Spectre.Console;

namespace ShiftsLogger.ConsoleUI.Controllers;
public class ShiftsController
{
    private readonly ShiftsService _shiftsService;

    public ShiftsController(ShiftsService shiftsService)
    {
        _shiftsService = shiftsService;
    }
    public async Task GetAllShifts()
    {
        var shifts = await _shiftsService.GetAll();
        if (shifts.Count == 0)
        {
            return;
        }

        TableVisualization.DisplayShiftsTable(shifts);
        var shift = GetShiftFromList(shifts);
    }

    public async Task AddShift()
    {
        var shift = CreateNewShift();
        TableVisualization.DisplayShiftTable(shift);
        if (!AnsiConsole.Confirm("Are you sure you want to create a new shift?"))
        {
            return;
        }

        await _shiftsService.CreateShift(shift);
    }

    public async Task DeleteShift()
    {
        var shifts = await _shiftsService.GetAll();
        if (shifts.Count == 0)
        {
            return;
        }

        TableVisualization.DisplayShiftsTable(shifts);
        var shift = GetShiftFromList(shifts);
        if (shift is null)
        {
            return;
        }

        TableVisualization.DisplayShiftTable(shift);
        if (!AnsiConsole.Confirm("Are you sure you want delete this shift?"))
        {
            return;
        }

        await _shiftsService.DeleteShift(shift.Id);
    }

    public async Task UpdateShift()
    {
        var shifts = await _shiftsService.GetAll();
        if (shifts.Count == 0)
        {
            return;
        }

        TableVisualization.DisplayShiftsTable(shifts);
        var shift = GetShiftFromList(shifts);
        if (shift is null)
        {
            return;
        }

        TableVisualization.DisplayShiftTable(shift);
        var shiftToUpdate = CreateShiftToUpdate(shift);
        if (!AnsiConsole.Confirm("Are you sure you want update this shift?"))
        {
            return;
        }

        await _shiftsService.UpdateShift(shift.Id, shiftToUpdate);
    }
    private Shift? GetShiftFromList(List<Shift> shifts)
    {
        var input = UserInput.PromptPositiveIntegerAllowZero("Enter shift ID (or press 0 to exit):");
        var index = Validation.IsValidListIndex(input, shifts);
        return shifts.ElementAtOrDefault(index);
    }

    private ShiftCreate CreateNewShift()
    {
        AnsiConsole.MarkupLine("Creating a new shift. Enter information:");
        AnsiConsole.MarkupLine("Format: [blue]YYYY-MM-DD HH:MM[/]");
        var startDateTime = UserInput.PromptDateTime("Start date and time:");
        var duration = UserInput.PromptPositiveInteger("Duration in hours:");
        var endDateTime = startDateTime.AddHours(duration);
        return new ShiftCreate(startDateTime, endDateTime);
    }

    private ShiftUpdate CreateShiftToUpdate(Shift shift)
    {
        AnsiConsole.MarkupLine("Enter new information:");
        AnsiConsole.MarkupLine("Format: [blue]YYYY-MM-DD HH:MM[/]");
        var startDateTime = UserInput.PromptDateTimeAllowEmpty("Start date and time (leave empty to skip):");
        if (startDateTime == DateTime.MinValue)
        {
            startDateTime = shift.StartTime;
        }
        var duration = UserInput.PromptPositiveInteger("Duration in hours:");
        var endDateTime = startDateTime.AddHours(duration);
        return new ShiftUpdate(startDateTime, endDateTime);

    }
}
