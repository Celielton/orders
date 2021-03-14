namespace OrdersApplications.SharedKernel
{
    public class Constants
    {
        public const string SEND_ORDER_MESSAGE = "Send order #{0} with a random number {1}";
        public const string AGENT_APP_MESSAGE = "I'm agent {0}, my magic number is {1}";
        public const string MAGIC_NUMBER_FOUND_MESSAGE = "Oh no, my magic number was found";
        public const string ORDER_RECEIVED = "Received OrderId #{0} - {1}";
        public const string CONFIRMATION_RECEIVED = "Confirmation for the Order #{0} processed by the Agent received {1} by the Supervisor";
        public const string API_URL = "https://localhost:5001/api";
        public const string QUEUE_CONNECTION = "queueconnection";
        public const string MESSAGE_PROCESSED = "processed";
        public const string CONFIRMATION_TABLE_NAME = "processed";
    }
}
