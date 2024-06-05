using RabbitMQExample.WorkerService;

#if DEBUG

// Em ambiente de produção ao instalar um worker no windows, não se deve utilizar propriedades de console!

Console.Title = "RabbitMQ Worker Consumer";

#endif

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
