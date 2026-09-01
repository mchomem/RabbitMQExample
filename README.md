# RabbitMQExample

Simple example of using RabbitMQ, containing a message publisher and a consumer (running in a Worker Service).

![Demo](/Docs/Images/Demo.png)

Follow the command line to upload the RabbitMQ image to Docker

`docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management`