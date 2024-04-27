using Microsoft.EntityFrameworkCore;
using MusicMS.Entities;

namespace MusicMS.Services.AlbumServices;

public class AlbumService : IAlbumService
{
	private readonly AppDbContext _context;

	public AlbumService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Album> Add(Album album)
	{
		await _context.AddAsync(album);
		await _context.SaveChangesAsync();

		return album;
	}

	public async Task<Album> GetById(string id)
	{
		return await _context.Albums
			.SingleOrDefaultAsync(x => x.Id.Equals(id));
	}
}