using MessageBroker;

Console.Title = "Darth Vader";

var broker = new Broker();
broker.MessageReceived += MessageReceived;
broker.ReadMessage(Constants.StarWarsQueueName);

void MessageReceived(string message)
{
    try
    {
        if(!string.IsNullOrEmpty(message))
        {
            var name = message.Split(',')[1].TrimStart().TrimEnd('.');

            Console.WriteLine($"Hello {name}, I am your father!");
        }
    }
    catch (Exception)
    {
        //TODO: log error.
    }
}

Console.ReadKey();
