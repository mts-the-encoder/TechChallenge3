using MusicMS.Entities;
using MusicMS.Services.AlbumServices;
using Newtonsoft.Json;

namespace MusicMS.EventProcessing.AlbumProcessor;

public class AddEventProcessor : IAddEventProcessor
{
	private readonly IServiceScopeFactory _factory;

	public AddEventProcessor(IServiceScopeFactory factory)
	{
		_factory = factory;
	}

	public async Task ProcessAddEvent(string message)
	{
		using (var scope = _factory.CreateScope())
		{
			var albumService = scope.ServiceProvider.GetRequiredService<IAlbumService>();

			var response = DeserializeMessage(message);

			if (response is not null)
			{
				await albumService.Add(response);
				Console.WriteLine("Album has been addd to the db");
			}
		}
	}

	private Album DeserializeMessage(string message)
	{
		var deserializeMessage = JsonConvert.DeserializeObject<Album>(message);
		return deserializeMessage;
	}
}