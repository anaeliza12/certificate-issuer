using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Api.Processor
{
    public class RabbitMQService
    {
        private readonly CertificateProcessor _generate;

        public RabbitMQService(CertificateProcessor generate)
        {
            _generate = generate;
        }

        public void StartListening()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "certificateQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await _generate.ProcessMessageAsync(message);
            };

            channel.BasicConsume(queue: "certificateQueue", autoAck: true, consumer: consumer);

            Console.WriteLine("Listening to RabbitMQ...");
            Console.ReadLine();
        }
    }
}
