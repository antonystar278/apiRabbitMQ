using Core.Entities;
using Core.Models.Operations;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace OperationsMessagingReceive
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
            var queueName = "InProcessOperationQueue";
            channel.QueueDeclare(queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var operationModel = JsonConvert.DeserializeObject<OperationModel>(content);

                Console.WriteLine(" [x] Received {0}", operationModel.Id);


                var newQueueName = "ComplitedOperationQueue";
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.QueueDeclare(queue: newQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var randomNumber = GetRandomNumber();

                var operationTime = TimeSpan.FromSeconds(randomNumber);
                var result = operationModel.ExecutionTime.Add(operationTime);
                operationModel.ExecutionTime = result;

                await Task.Delay(randomNumber * 1000);
                var json = JsonConvert.SerializeObject(operationModel);
                var body = Encoding.UTF8.GetBytes(json);
                Console.WriteLine(operationModel.ExecutionTime);

                channel.BasicPublish(exchange: "", routingKey: newQueueName, basicProperties: properties, body: body);

                Console.WriteLine(" [x] Done");

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queueName,
                                 autoAck: false,
                                 consumer: consumer);

            int GetRandomNumber()
            {
                Random rnd = new Random();

                int randomNumber = rnd.Next(7, 15);
                return randomNumber;
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
