namespace RabbitMQExample.Shared.Models;

public sealed class Car
{
    public Car(Guid id, string name, DateTime manufacturingDate)
    {
        Id = id;
        Name = name;
        ManufacturingDate = manufacturingDate;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime ManufacturingDate { get; private set; }
}
