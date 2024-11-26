using System.ComponentModel.DataAnnotations;
using ShiftsLogger.Common.Validation;

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
