using Core.Entities;
using Core.Models.Operations;
using Core.Services;
using Infrastructure.AppContext;
using Infrastructure.Repositories.Repositories.EFRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Operations.Messaging.Send.Options;
using Operations.Messaging.Send.Sender;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TaskHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var newQueueName = "OperationTimeUpadateQueue";
            channel.QueueDeclare(newQueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] We are in Taskhandler().");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (sender, ea) =>
            {
                var builder = new DbContextOptionsBuilder<AppDbContext>();
                builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=operationsdb;Trusted_Connection=True;Integrated Security=True;MultipleActiveResultSets=true;");
                var context = new AppDbContext(builder.Options);
                var repository = new OperationRepository(context);

                var config = new RabbitMqConfiguration { Hostname = "localhost", QueueName = "OperationTimeUpadateQueue" };
                var options = Options.Create<RabbitMqConfiguration>(config);
                var operationUpdateSender = new OperationUpdateSender(options);
                var operationService = new OperationService(repository, operationUpdateSender);
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var operationModel = JsonConvert.DeserializeObject<OperationModel>(content);

                Console.WriteLine($" [x] Received ExecutionTime = {operationModel.ExecutionTime} and TaskId = {operationModel.Id}");

                Console.WriteLine(" [x] Done");

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                await operationService.UpdateAsync(operationModel);

            };

            channel.BasicConsume(newQueueName,
                             autoAck: false,
                             consumer: consumer);
            Console.ReadLine();
        }
    }
}
