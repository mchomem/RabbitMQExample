namespace RabbitMQExample.Shared.Models;

public class Car
{
    public Car(int id, string name, DateTime manufacturingDate)
    {
        Id = id;
        Name = name;
        ManufacturingDate = manufacturingDate;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ManufacturingDate { get; set; }
}
