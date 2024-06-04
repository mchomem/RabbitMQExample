Console.WriteLine("RabbitMQ - publishing ...");

var car = new Car(1, "Ferrari", new DateTime(2024, 06, 03));
new Publisher().Publish<Car>(car);

Console.WriteLine("RabbitMQ - done");
