using MessageBroker;

Console.Title = "Darth Vader";

Broker.MessageReceived += MessageReceived;
Broker.ReadMessage(Constants.StarWarsQueueName);

static void MessageReceived(string message)
{
    try
    {
        if(!string.IsNullOrEmpty(message))
        {
            var name = message.Split(',')[1].TrimStart();

            Console.WriteLine($"Hello {name}, I am your father!");
        }
    }
    catch (Exception)
    {
        //TODO: log error.
    }
}

Console.ReadKey();
