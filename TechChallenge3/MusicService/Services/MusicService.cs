using Microsoft.EntityFrameworkCore;
using MusicMS.Entities;

namespace MusicMS.Services;

public class MusicService : IMusicService
{
	private readonly AppDbContext _context;

	public MusicService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Music> Add(Music music)
	{
		await _context.Musics.AddAsync(music);
		await _context.SaveChangesAsync();

		return music;
	}

	public Music GetById(string id)
	{
		return _context.Musics.AsNoTracking().SingleOrDefault(x => x.Id.Equals(id));
	}
}