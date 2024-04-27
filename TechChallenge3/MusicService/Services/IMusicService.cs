namespace MusicMS.Services;

public interface IMusicService
{
    Task<Entities.Music> Add(Entities.Music music);
    Entities.Music GetById(string id);
}