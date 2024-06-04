namespace ConsoleAppConsumer;

public class Consumer
{
    private ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;
    private const string QUEUE = "queue-netcore";

    public Consumer()
    {
        _connectionFactory = new ConnectionFactory { HostName = "localhost" };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    // FIX: não está consumindo a mensagem publicada.
    public void Consume()
    {
        // 1 erro - faltou declarar a fila, blz corrigido!
        _channel.QueueDeclare
        (
            queue: QUEUE,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var message = JsonSerializer.Deserialize<Car>(contentString);

            Console.WriteLine($"Message received: {message}");

            _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue: QUEUE, autoAck: false, consumer: consumer);
    }
}
