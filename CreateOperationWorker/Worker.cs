using Core.Models.Operations;
using CreateOperationWorker.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreateOperationWorker
{
    public class Worker : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly RabbitMqConfiguration _rabbitMqOptions;

        public Worker(IConfiguration configuration)
        {
            _rabbitMqOptions = configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>();

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqOptions.Hostname
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _rabbitMqOptions.InProcesOperationQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            Console.WriteLine(" [*] Waiting for messages.");
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var operationModel = JsonConvert.DeserializeObject<OperationCreateMessageModel>(content);
                Console.WriteLine(" [x] Received {0}", operationModel.Id);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;
                _channel.QueueDeclare(queue: _rabbitMqOptions.ComplitedOperationQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var randomNumber = GetRandomNumber();

                var operationTime = TimeSpan.FromSeconds(randomNumber);
                var result = operationModel.ExecutionTime.Add(operationTime);
                operationModel.ExecutionTime = result;

                await Task.Delay(randomNumber * 1000);
                var json = JsonConvert.SerializeObject(operationModel);
                var body = Encoding.UTF8.GetBytes(json);
                Console.WriteLine(operationModel.ExecutionTime);

                _channel.BasicPublish(exchange: "", routingKey: _rabbitMqOptions.ComplitedOperationQueue, basicProperties: properties, body: body);

                Console.WriteLine(" [x] Done");

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_rabbitMqOptions.InProcesOperationQueue, false, consumer);

            return Task.CompletedTask;
        }

        private int GetRandomNumber()
        {
            Random rnd = new Random();

            int randomNumber = rnd.Next(7, 15);
            return randomNumber;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
