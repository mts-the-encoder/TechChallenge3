using AlbumMS.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlbumMS.Services;

public class AlbumService : IAlbumService
{
	private readonly AppDbContext _context;

	public AlbumService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Album> Add(Album music)
	{
		await _context.Albums.AddAsync(music);
		await _context.SaveChangesAsync();

		return music;
	}

	public Album GetById(string id)
	{
		return _context.Albums
			.AsNoTracking()
			.SingleOrDefault(x => x.Id.Equals(id));
	}
}