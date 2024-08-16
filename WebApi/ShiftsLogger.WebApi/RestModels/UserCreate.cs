namespace ShiftsLogger.WebApi.RestModels;

public class UserCreate
{
    public Guid ShiftId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
