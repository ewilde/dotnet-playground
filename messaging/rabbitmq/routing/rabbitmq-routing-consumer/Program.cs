namespace rabbitmq.routing.consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            new ReceiveLogsDirect().Run(args);
        }
    }
}
