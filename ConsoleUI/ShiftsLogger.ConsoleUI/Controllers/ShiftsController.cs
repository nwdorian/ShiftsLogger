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
        var shift = Helpers.GetShiftFromList(shifts);
    }

    public async Task AddShift()
    {
        var shift = Helpers.CreateNewShift();
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
        var shift = Helpers.GetShiftFromList(shifts);
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
        var shift = Helpers.GetShiftFromList(shifts);
        if (shift is null)
        {
            return;
        }

        TableVisualization.DisplayShiftTable(shift);
        var shiftToUpdate = Helpers.CreateShiftToUpdate(shift);
        if (!AnsiConsole.Confirm("Are you sure you want update this shift?"))
        {
            return;
        }

        await _shiftsService.UpdateShift(shift.Id, shiftToUpdate);
    }
}
