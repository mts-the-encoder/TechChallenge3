using AlbumMS.Entities;

namespace AlbumMS.Services;

public interface IAlbumService
{
    Task<Album> Add(Album album);
    Album GetById(string id);
}