using AlbumMS.Entities;

namespace AlbumMS.ServiceBus;

public interface IAlbumServiceBus
{
	void PublishNewAlbum(Album album);
}