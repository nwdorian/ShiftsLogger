namespace ShiftsLogger.ConsoleUI.Models;
public class Shift : IShift
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
