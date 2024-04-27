using MusicMS.Entities;

namespace MusicMS.Services.MusicServices;

public interface IMusicService
{
	Task<Music> Add(Music music);
	Task<IEnumerable<Music>> GetByAlbumId(string id);
	Task<Album> ExistAlbum(string id);
}