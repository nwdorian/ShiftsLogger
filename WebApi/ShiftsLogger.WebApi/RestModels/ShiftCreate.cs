using ShiftsLogger.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.WebApi.RestModels;

public class ShiftCreate
{
    [Required]
    [Display(Name = "Shift start date and time")]
    public DateTime StartTime { get; set; }

    [Required]
    [Display(Name = "Shift end date and time")]
    [DateGreaterThan("StartTime")]
    public DateTime EndTime { get; set; }
}
