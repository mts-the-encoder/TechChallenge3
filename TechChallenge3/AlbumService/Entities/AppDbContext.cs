using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AlbumMS.Entities;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		
	}

	public DbSet<Album> Albums { get; set; }
	public DbSet<Music> Musics { get; set; }
}