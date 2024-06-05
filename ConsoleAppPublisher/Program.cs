Console.Title = "Rabbit MQ Publisher";

ShowMenu();

static void ShowMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Choose an option");
        Console.WriteLine("1 - Publish 1 message in RabbitMQ");
        Console.WriteLine("2 - Publish 10 messages in RabbitMQ");
        Console.WriteLine("3 - Publish 100 messages in RabbitMQ");
        Console.WriteLine("0 - Exit");
        Console.Write("Option: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                ExecuteXTimes(1); break;

            case "2":
                ExecuteXTimes(10); break;

            case "3":
                ExecuteXTimes(100); break;

            case "0":
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Choose any option from menu. Pressa any key to continue...");
                Console.ReadKey();
                break;
        }

        Console.ReadKey();
    }
}

static void ExecuteXTimes(int times)
{
    Console.WriteLine("RabbitMQ - publishing ...");
    var random = new Random();
    List<string> carNames = new List<string> { "Porche", "Ferrari", "Volkswagen", "Toyota", "Honda" };

    for (int i = 0; i < times; i++)
    {
        var car = new Car(Guid.NewGuid(), carNames.ElementAt(random.Next(0, carNames.Count - 1)), new DateTime(random.Next(1999, 2024), random.Next(1, 12), random.Next(1, 28)));
        new Publisher().Publish<Car>(car);
    }

    Console.WriteLine("RabbitMQ - done, press any button.");
}
