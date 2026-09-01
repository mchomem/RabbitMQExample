namespace RabbitMQExample.WorkerService;

public sealed class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private const string QUEUE = "queue-netcore";
    private readonly IModel _channel;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
        var connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("Worker running at: {0}", DateTimeOffset.Now);

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

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nReceived data: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\n{0}\n", IdentJson(message));

                _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: QUEUE,
                                 autoAck: false,
                                 consumer: consumer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Consume done.");
            Console.ForegroundColor = ConsoleColor.Gray;

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private string IdentJson(string content)
    {
        using var document = JsonDocument.Parse(content);
        string indentedJson = JsonSerializer.Serialize(document, new JsonSerializerOptions { WriteIndented = true });
        return indentedJson;
    }
}
