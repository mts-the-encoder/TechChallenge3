using System.Text;
using MusicMS.EventProcessing.AlbumProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MusicMS.MusicBackgroundService.AlbumBackgroundProcessor;

public class AlbumMessageSubscriber : BackgroundService
{
	private readonly IConfiguration _configuration;
	private readonly IAddEventProcessor _eventProcessor;
	private IConnection _connection;
	private IModel _channel;
	private string _queueName;

	public AlbumMessageSubscriber(IConfiguration configuration, IAddEventProcessor eventProcessor)
	{
		_configuration = configuration;
		_eventProcessor = eventProcessor;
		InitializeRabbitMq();
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();
		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += (ModuleHandle, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);
			_eventProcessor.ProcessAddEvent(message);
			Console.WriteLine($"Service Received new message {message}");
		};

		if (_queueName is not null)
			_channel.BasicConsume(_queueName, true, consumer);

		return Task.CompletedTask;
	}

	//ta quebrando ali no meio, ja meti o erro lpa no stack, pesquisar e ver como resolver isso

	private void InitializeRabbitMq()
	{
		var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };

		try
		{
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.ExchangeDeclare("AlbumExchange", ExchangeType.Direct);
			_queueName = "Album";

			// Declare a fila explicitamente
			_channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

			// Vincule a fila ao exchange
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
		Console.WriteLine("Connection shutdown");
	}

	public override void Dispose()
	{
		if (_channel.IsOpen)
		{
			_channel.Close();
			_connection.Close();
		}
		base.Dispose();
	}
}