using Microsoft.AspNetCore.Mvc;
using MusicMS.Entities;
using MusicMS.Services;

namespace MusicMS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MusicController : ControllerBase
{
	private readonly IMusicService _service;

	public MusicController(IMusicService service)
	{
		_service = service;
	}

	[HttpPost]
	public async Task<IActionResult> Create(Music music)
	{
		var response = await _service.Add(music);

		return response is not null
			? Ok(response)
			: BadRequest("Error on request");
	}

	[HttpGet]
	public IActionResult GetById(string id)
	{
		var response = _service.GetById(id);

		return response is not null
			? Ok(response)
			: NotFound("Music not found");
	}
}