namespace rabbitmq.topics.consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            new ReceiveLogsTopic(args);
        }
    }
}
