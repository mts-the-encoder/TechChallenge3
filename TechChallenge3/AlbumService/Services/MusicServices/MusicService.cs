using Microsoft.EntityFrameworkCore;
using MusicMS.Entities;

namespace MusicMS.Services.MusicServices;

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

	public async Task<IEnumerable<Music>> GetByAlbumId(string id)
	{
		return await _context.Musics
			.AsNoTracking()
			.Where(x => x.Id.Equals(id))
			.ToListAsync();
	}

	public async Task<Album> ExistAlbum(string id)
	{
		return await _context.Albums.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
	}
}