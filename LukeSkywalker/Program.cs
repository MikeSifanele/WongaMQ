using MessageBroker;

Console.Title = "Luke";

var message = "Hello my name is, Luke.";

Broker.SendMessage(message);
