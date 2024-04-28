namespace MusicMS.EventProcessing.AlbumProcessor;

public interface IAddEventProcessor
{
	Task ProcessAddEvent(string message);
}