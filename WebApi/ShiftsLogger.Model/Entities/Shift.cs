using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Model.Entities;
public class Shift
{
    public Guid Id { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    public List<User> Users { get; set; } = null!;
}
