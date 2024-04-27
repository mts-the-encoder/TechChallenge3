using AlbumMS.Entities;
using AlbumMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlbumMS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AController : ControllerBase
{
	private readonly IAlbumService _service;

	public AController(IAlbumService service)
	{
		_service = service;
	}

	[HttpPost]
	public async Task<IActionResult> Create(Album album)
	{
		var response = await _service.Add(album);

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
			: NotFound("Album not found");
	}
}