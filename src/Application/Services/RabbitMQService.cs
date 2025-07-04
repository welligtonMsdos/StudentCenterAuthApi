using RabbitMQ.Client;
using StudentCenterAuthApi.src.Application.DTOs;
using StudentCenterAuthApi.src.Application.Interfaces;
using System.Text;
using System.Text.Json;

namespace StudentCenterAuthApi.src.Application.Services;

public class RabbitMQService : IRabbitMQService
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService(IConfiguration configuration)
    {
        _configuration = configuration;

        _connection = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]),
            UserName = _configuration["RabbitMQUser"],
            Password = _configuration["RabbitMQPassword"]
        }.CreateConnection();

        _channel = _connection.CreateModel();

        //_channel.ExchangeDeclare("trigger", ExchangeType.Fanout, durable: false, autoDelete: false, arguments: null);

        _channel.ExchangeDeclare(
            exchange: "trigger",
            type: ExchangeType.Fanout,
            durable: false,
            autoDelete: false,
            arguments: null
        );

        _channel.QueueDeclare(
            queue: "trigger",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        _channel.QueueBind(
            queue: "trigger",
            exchange: "trigger",
            routingKey: ""
        );
    }

    public Task<bool> PublishMessage(UserDto userDto)
    {
        var message = JsonSerializer.Serialize(userDto);

        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "trigger",
                              routingKey: "",
                              basicProperties: null,
                              body: body);

        return Task.FromResult(true);
    }
}
