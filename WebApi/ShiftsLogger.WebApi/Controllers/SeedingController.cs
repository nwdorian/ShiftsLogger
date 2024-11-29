using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Service.Common;

namespace ShiftsLogger.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SeedingController : ControllerBase
{
	private readonly ISeedingService _seedingService;

	public SeedingController(ISeedingService seedingService)
	{
		_seedingService = seedingService;
	}
	[HttpPost]
	public async Task<ActionResult> SeedData()
	{
		var response = await _seedingService.SeedDataAsync();

		if (response.Success)
		{
			return Ok(response.Message);
		}

		return BadRequest(response.Message);
	}
}
