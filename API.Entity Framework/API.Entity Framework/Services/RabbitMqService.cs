using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using API.Entity_Framework.Dtos;

namespace API.Entity_Framework.Services
{
    public class RabbitMqService
    {
        private readonly RabbitMQ.Client.IModel _channel;

        public RabbitMqService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare(queue: "API Students", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void PublishAluno(StudentDto aluno)
        {
            var mensagem = JsonSerializer.Serialize(aluno);
            var corpo = Encoding.UTF8.GetBytes(mensagem);

            _channel.BasicPublish(exchange: string.Empty, routingKey: "API Students", basicProperties: null, body: corpo);
        }
    }
}