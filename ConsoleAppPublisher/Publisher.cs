using RabbitMQ.Client;

namespace ConsoleAppPublisher;

public class Publisher
{
    const string EXCHANGE = "exchange-netcore";
    private ConnectionFactory _connectionFactory;

    public Publisher()
        => _connectionFactory = new ConnectionFactory() { HostName = "localhost" };

    public void Publish(string queue, byte[] message)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                // Garantir que a fila esteja criada
                channel.QueueDeclare(
                    queue: queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                // Publicar a mensagem
                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: queue,
                    basicProperties: null,
                    body: message);
            }
        }
    }
}
