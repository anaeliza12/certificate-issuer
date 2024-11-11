using Api.Certification.AppDomain.Commands.request;
using Api.Certification.AppDomain.Interfaces;
using Api.Certification.AppDomain.Model;
using Api.Certification.AppDomain.Utils.AppSettings;
using Api.Certification.Infra.ApiSettings.Repositories.Context;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Api.Certification.Infra.Services
{
    public class GenerateCertificateService : IGenerateCertificateService
    {
        private readonly MySQLContext _Dbcontext;
        public GenerateCertificateService(MySQLContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }
        public async Task GenerateCertificateAsync(GenerateCertificateRequest request)
        {
            SendToQueue(request);
            await SaveCertificateStudentAsync(request.StudentModel);
        }

        private static void SendToQueue(GenerateCertificateRequest request)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: "certificateQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

                string jsonRequest = JsonConvert.SerializeObject(request);
                var body = Encoding.UTF8.GetBytes(jsonRequest);

                channel.BasicPublish(exchange: "", routingKey: "certificateQueue", basicProperties: null, body: body);

                Console.WriteLine("Message sent to queue successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending message to queue" + ex);
            }
        }
        public async Task<StudentModel> SaveCertificateStudentAsync(StudentModel student)
        {
            var studentSaved = _Dbcontext.Student.Add(student);
            var rowsAffected = await _Dbcontext.SaveChangesAsync();

            if (rowsAffected < 1)
            {
                throw new Exception("It was not possible to save: " + student.Name + " in database");
            }

            return studentSaved.Entity;
        }
    }
}
