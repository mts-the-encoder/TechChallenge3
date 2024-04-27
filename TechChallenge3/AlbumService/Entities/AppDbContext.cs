using Microsoft.EntityFrameworkCore;

namespace MusicMS.Entities;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		
	}

	public DbSet<Album> Albums { get; set; }
	public DbSet<Music> Musics { get; set; }
}