namespace AlbumMS.Entities;

public class Album
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string ReleaseYear { get; set; }
	public string MusicId { get; set; }
	public Music Music { get; set; }
	public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}