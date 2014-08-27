namespace rabbitmq.hello.consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Receive().Run();
        }
    }
}
