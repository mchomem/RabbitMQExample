using ConsoleAppConsumer;

Console.WriteLine("RabbitMQ - comsumming");

new Consumer().Consume();

Console.WriteLine("RabbitMQ - done");
