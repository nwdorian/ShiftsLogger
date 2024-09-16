namespace ShiftsLogger.DAL.Entities;
public class ShiftEntity
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public ICollection<UserShiftEntity>? Users { get; set; }
}
