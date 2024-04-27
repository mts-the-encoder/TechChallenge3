using System.ComponentModel.DataAnnotations;

namespace AlbumMS.Entities;

public class Album
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	[Required]
	public string Artist { get; set; }
	[Required]
	public string Title { get; set; }

	public DateTime CreatedOn { get; set; } = DateTime.Now;
}