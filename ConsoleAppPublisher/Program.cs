Console.WriteLine("RabbitMQ - publishing ...");

var car = new Car(1, "Ferrari", new DateTime(2024, 06, 03));
new Publisher().Publish<Car>(car);

Console.WriteLine("RabbitMQ - done");

//using RabbitMQ.Client;
//using System.Text;

//namespace RabbitMQPublisher
//{
//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            var factory = new ConnectionFactory() { HostName = "localhost" };
//            using (var connection = factory.CreateConnection())
//            using (var channel = connection.CreateModel())
//            {
//                string exchangeName = "my_exchange";
//                string queueName = "my_queue";
//                string routingKey = "my_routing_key";

//                // Declara a exchange
//                channel.ExchangeDeclare(exchange: exchangeName, type: "direct");

//                // Declara a fila
//                channel.QueueDeclare(queue: queueName,
//                                     durable: false,
//                                     exclusive: false,
//                                     autoDelete: false,
//                                     arguments: null);

//                // Faz o binding da fila com a exchange usando a routing key
//                channel.QueueBind(queue: queueName,
//                                  exchange: exchangeName,
//                                  routingKey: routingKey);

//                // Publica a mensagem
//                string message = "Hello RabbitMQ!";
//                var body = Encoding.UTF8.GetBytes(message);

//                channel.BasicPublish(exchange: exchangeName,
//                                     routingKey: routingKey,
//                                     basicProperties: null,
//                                     body: body);

//                Console.WriteLine(" [x] Sent {0}", message);
//            }
//        }
//    }
//}
