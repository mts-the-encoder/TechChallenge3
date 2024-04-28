using System.Text;
using System.Text.Json;
using AlbumMS.Entities;
using RabbitMQ.Client;

namespace AlbumMS.ServiceBus;

public class AlbumServiceBus : IAlbumServiceBus
{
	private readonly IConfiguration _configuration;
	private readonly IConnection _connection;
	private readonly IModel _channel;
	private string _queueName;

	public AlbumServiceBus(IConfiguration configuration)
	{
		_configuration = configuration;

		var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };

		try
		{
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.ExchangeDeclare("AlbumExchange", ExchangeType.Direct);
			_queueName = "Album";

			_channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

			_channel.QueueBind(_queueName, "AlbumExchange", "AlbumKey");

			_connection.ConnectionShutdown += _connection_ConnectionShutdown;
			Console.WriteLine("Connection created");
		}
		catch (Exception e)
		{
			Console.WriteLine($"Couldn't connect to rabbitmq: reason: {e.Message}");
			throw;
		}
	}

	private void _connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
	{
		Console.WriteLine("Connection has been shutdown");
	}

	public void PublishNewAlbum(Album album)
	{
		var message = JsonSerializer.Serialize(album);

		if (_connection is not null)
		{
			if (_connection.IsOpen)
			{
				Console.WriteLine("RabbitMQ conn is open");
				var response = SendMessage(message);

				if (response) Console.WriteLine($"{message} has been sent");
			}
		}
	}

	private bool SendMessage(string message)
	{
		try
		{
			var body = Encoding.UTF8.GetBytes(message);
			_channel.BasicPublish("AlbumExchange", "AlbumKey", null, body);
			return true;
		}
		catch (Exception e)
		{
			return false;
		}
	}
}