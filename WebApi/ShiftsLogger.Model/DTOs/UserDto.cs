namespace ShiftsLogger.Model.DTOs;
public class UserDto
{
    public Guid Id { get; set; }
    public Guid ShiftId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}
