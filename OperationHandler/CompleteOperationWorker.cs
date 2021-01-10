using Core.Interfaces.Core.Services;
using Core.Models.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OperationHandler.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OperationHandler
{
    public class CompleteOperationWorker: BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly RabbitMqConfiguration _rabbitMqOptions;
        private readonly IOperationService _operationService;

        public CompleteOperationWorker(IConfiguration configuration, IOperationService operationService)
        {
            _operationService = operationService;
            _rabbitMqOptions = configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>(); ;

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
            _channel.QueueDeclare(queue: _rabbitMqOptions.ComplitedOperationQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);
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

                Console.WriteLine($" [x] Received ExecutionTime = {operationModel.ExecutionTime} and TaskId = {operationModel.Id}");

                Console.WriteLine(" [x] Done");

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                await _operationService.UpdateAsync(operationModel);

            };

            _channel.BasicConsume(_rabbitMqOptions.ComplitedOperationQueue, false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
