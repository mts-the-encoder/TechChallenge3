using Microsoft.EntityFrameworkCore;

namespace AlbumMS.Entities;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Album> Albums { get; set; }
}