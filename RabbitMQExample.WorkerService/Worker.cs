using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQExample.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private const string QUEUE = "queue-netcore";
    private ConnectionFactory _connectionFactory;
    private IModel _channel;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
        var connection = _connectionFactory.CreateConnection();
        _channel = connection.CreateModel();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            // Declara a fila
            _channel.QueueDeclare(queue: QUEUE,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            // Configura o consumer para receber mensagens da fila
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received {0}", message);

                _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: QUEUE,
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine("Consume done.");

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
