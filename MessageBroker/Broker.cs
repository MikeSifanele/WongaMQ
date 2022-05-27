using RabbitMQ.Client;
using System.Text;

namespace MessageBroker
{
    public static class Broker
    {
        public static void SendMessage(string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                    return;

                var connectionFactory = new ConnectionFactory
                {
                    Uri = new Uri(Constants.GuestUriString)
                };

                using (var connection = connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(Constants.StarWarsQueueName, durable: true, exclusive: false, autoDelete: false);

                        channel.BasicPublish(string.Empty, Constants.StarWarsQueueName, null, Encoding.UTF8.GetBytes(message));
                    }
                }
            }
            catch (Exception)
            {
                //TODO: Log error.
            }
        }
    }
}
