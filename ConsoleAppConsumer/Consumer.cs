namespace ConsoleAppConsumer;

public class Consumer
{
    private const string QUEUE = "queue-netcore";
    private ConnectionFactory _connectionFactory;

    public Consumer()
        => _connectionFactory = new ConnectionFactory() { HostName = "localhost" };

    public void Consume()
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                // Declara a fila
                channel.QueueDeclare(queue: QUEUE,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Configura o consumer para receber mensagens da fila
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                };

                channel.BasicConsume(queue: QUEUE,
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
