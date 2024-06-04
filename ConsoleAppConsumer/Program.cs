using ConsoleAppConsumer;

Console.WriteLine("RabbitMQ - comsumming");

new Consumer().Consume();

Console.WriteLine("RabbitMQ - done");

//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;

//namespace RabbitMQConsumer
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var factory = new ConnectionFactory() { HostName = "localhost" };
//            using (var connection = factory.CreateConnection())
//            using (var channel = connection.CreateModel())
//            {
//                string queueName = "my_queue";

//                // Declara a fila
//                channel.QueueDeclare(queue: queueName,
//                                     durable: false,
//                                     exclusive: false,
//                                     autoDelete: false,
//                                     arguments: null);

//                // Configura o consumer para receber mensagens da fila
//                var consumer = new EventingBasicConsumer(channel);
//                consumer.Received += (model, ea) =>
//                {
//                    var body = ea.Body.ToArray();
//                    var message = Encoding.UTF8.GetString(body);
//                    Console.WriteLine(" [x] Received {0}", message);
//                };

//                channel.BasicConsume(queue: queueName,
//                                     autoAck: true,
//                                     consumer: consumer);

//                Console.WriteLine(" Press [enter] to exit.");
//                Console.ReadLine();
//            }
//        }
//    }
//}
