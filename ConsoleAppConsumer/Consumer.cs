namespace ConsoleAppConsumer;

public class Consumer
{
    private ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;

    public Consumer()
    {
        _connectionFactory = new ConnectionFactory { HostName = "localhost" };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    // FIX: não está consumindo a mensagem publicada.
    public void Consume(string queue)
    {
        // 1 erro - faltou declarar a fila, blz corrigido!
        _channel.QueueDeclare(queue: queue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var message = JsonSerializer.Deserialize<Car>(contentString);

            Console.WriteLine($"Message received: {message}");

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
    }
}
