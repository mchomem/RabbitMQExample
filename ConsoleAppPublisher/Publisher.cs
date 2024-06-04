using RabbitMQ.Client;

namespace ConsoleAppPublisher;

public class Publisher
{
    private const string EXCHANGE = "exchange-netcore";
    private const string ROUTING_KEY = "rounting-key-netcore";
    private const string QUEUE = "queue-netcore";
    private ConnectionFactory _connectionFactory;

    public Publisher()
        => _connectionFactory = new ConnectionFactory() { HostName = "localhost" };

    public void Publish<T>(T message)
    {
        var objectJsonString = JsonSerializer.Serialize(message);
        var objectToBytes = Encoding.UTF8.GetBytes(objectJsonString);

        using (var connection = _connectionFactory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                // Declarar a exchange.
                channel.ExchangeDeclare(exchange: EXCHANGE, type: "topic");

                // Garantir que a fila esteja criada.
                channel.QueueDeclare
                (
                    queue: QUEUE,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                // Faz o binding da fila com a exchange usando a routing key
                channel.QueueBind(queue: QUEUE, exchange: EXCHANGE, routingKey: ROUTING_KEY);

                // Publicar a mensagem.
                channel.BasicPublish
                (
                    exchange: EXCHANGE,
                    routingKey: ROUTING_KEY,
                    basicProperties: null,
                    body: objectToBytes
                );
            }
        }
    }
}
