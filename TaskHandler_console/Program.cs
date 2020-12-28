using Core.Entities;
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

namespace TaskHandler_console
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
                var senser = new OperationUpdateSender(options);
                var operationService = new OperationService(repository, senser);
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var operation = JsonConvert.DeserializeObject<Operation>(content);

                Console.WriteLine($" [x] Received ExecutionTime = {operation.ExecutionTime} and Task = {operation.Name}");

                Console.WriteLine(" [x] Done");

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                await operationService.UpdateAsync(operation);

            };

            channel.BasicConsume(newQueueName,
                             autoAck: false,
                             consumer: consumer);
            Console.ReadLine();
        }
    }
}
