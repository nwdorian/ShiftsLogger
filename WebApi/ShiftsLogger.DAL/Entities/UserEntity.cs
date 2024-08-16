using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLogger.DAL.Entities;
[Table("User")]
public class UserEntity
{
    public Guid Id { get; set; }
    public Guid ShiftId { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? FirstName { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Email { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    [Required]
    public DateTime DateUpdated { get; set; }
    public ShiftEntity Shift { get; set; } = null!;
}
