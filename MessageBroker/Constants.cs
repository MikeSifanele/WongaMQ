namespace MessageBroker
{
    public static class Constants
    {
        /// <summary>
        /// Default uri, would split in production app.
        /// </summary>
        public static string GuestUriString = "amqp://guest:guest@localhost:5672";
        public static string StarWarsQueueName = "starwars-queue";
    }
}
