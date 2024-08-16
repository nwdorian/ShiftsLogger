namespace ShiftsLogger.WebApi.RestModels;

public class ShiftRead
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
