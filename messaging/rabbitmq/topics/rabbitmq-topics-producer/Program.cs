namespace rabbitmq.topics.producer
{
    class Program
    {
        static void Main(string[] args)
        {
             new EmitLogTopic().Run(args);
        }
    }
}
