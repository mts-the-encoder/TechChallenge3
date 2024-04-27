namespace MusicMS.Entities;

public class Album
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string Artist { get; set; }
	public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}