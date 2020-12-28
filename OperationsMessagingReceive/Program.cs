using Core.Entities;
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
            var queueName = "OperationQueue";
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
                var operation = JsonConvert.DeserializeObject<Operation>(content);

                Console.WriteLine(" [x] Received {0}", operation.Name);


                var newQueueName = "OperationTimeUpadateQueue";
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.QueueDeclare(queue: newQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var randomNumber = GetRandomNumber();

                var operationTime = TimeSpan.FromSeconds(randomNumber);
                var result = operation.ExecutionTime.Add(operationTime);
                operation.ExecutionTime = result;

                await Task.Delay(randomNumber * 1000);
                var json = JsonConvert.SerializeObject(operation);
                var body = Encoding.UTF8.GetBytes(json);
                Console.WriteLine(operation.ExecutionTime);

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
