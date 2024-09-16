namespace ShiftsLogger.DAL.Entities;
public class UserShiftEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ShiftId { get; set; }
    public UserEntity? User { get; set; }
    public ShiftEntity? Shift { get; set; }

}
