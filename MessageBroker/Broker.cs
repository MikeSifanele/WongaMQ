using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessageBroker
{
    public static class Broker
    {
        #region Delegates
        public delegate void MessageReceivedHandler(string message);
        public static event MessageReceivedHandler MessageReceived;
        #endregion
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

        public static void ReadMessage(string queueName)
        {
            try
            {
                var connectionFactory = new ConnectionFactory
                {
                    Uri = new Uri(Constants.GuestUriString)
                };

                using (var connection = connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += ConsumerReceivedHandler;

                        channel.BasicConsume(Constants.StarWarsQueueName, autoAck: true, consumer);
                    }
                }
            }
            catch (Exception)
            {
                //TODO: Log error.
            }
        }
        private static void ConsumerReceivedHandler(object? sender, BasicDeliverEventArgs e)
        {
            try
            {
                var body = e.Body.ToArray();

                if (body.Length > 0)
                {
                    var message = Encoding.UTF8.GetString(body);

                    MessageReceived?.Invoke(message);
                }
            }
            catch (Exception)
            {
                //TODO: Log error.
            }
        }
    }
}
