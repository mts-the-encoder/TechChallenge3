using MusicMS.Entities;

namespace MusicMS.Services.AlbumServices;

public interface IAlbumService
{
	Task<Album> Add(Album album);
	Task<Album> GetById(string id);
}