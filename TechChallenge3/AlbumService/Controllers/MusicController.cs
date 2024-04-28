using Microsoft.AspNetCore.Mvc;
using MusicMS.Entities;
using MusicMS.Services.MusicServices;

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
			: BadRequest("Error on create Music");
	}

	[HttpGet]
	public async Task<IActionResult> GetAllByAlbumId(string id)
	{
		var response = await _service.GetByAlbumId(id);

		return response is not null
			? Ok(response)
			: NotFound("Album not found");
	}
}