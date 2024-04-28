using AlbumMS.Entities;
using AlbumMS.ServiceBus;
using AlbumMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlbumMS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
	private readonly IAlbumService _service;
	private readonly IAlbumServiceBus _serviceBus;

	public AlbumController(IAlbumService service, IAlbumServiceBus serviceBus)
	{
		_service = service;
		_serviceBus = serviceBus;
	}

	[HttpPost]
	public async Task<IActionResult> Create(Album album)
	{
		var response = await _service.Add(album);

		if (response is not null)
		{
			_serviceBus.PublishNewAlbum(response);
			return Ok(response);
		}
		return BadRequest("Error on request");
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