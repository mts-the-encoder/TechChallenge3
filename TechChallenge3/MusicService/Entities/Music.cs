namespace MusicMS.Entities;

public class Music
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string Artist { get; set; }
	public string Title { get; set; }
	public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}