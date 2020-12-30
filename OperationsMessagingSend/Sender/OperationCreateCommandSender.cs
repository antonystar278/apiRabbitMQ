using Core.Entities;
using Core.Interfaces.Operations.Messaging.Send;
using Core.Models.Operations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Operations.Messaging.Send.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Messaging.Send.Sender
{
    public class OperationCreateCommandSender : IOperationUpdateSender
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private IConnection _connection;

        public OperationCreateCommandSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _queueName = rabbitMqOptions.Value.QueueName;
            _hostname = rabbitMqOptions.Value.Hostname;

            CreateConnection();
        }

        public async Task SendOperation(OperationModel model)
        {

            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonConvert.SerializeObject(model);
                    var body = Encoding.UTF8.GetBytes(json);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    await Task.Run(() =>
                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: properties, body: body));
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}
