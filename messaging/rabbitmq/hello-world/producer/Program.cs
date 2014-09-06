using System;

namespace rabbitmq.hello.producer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Send().Run();
            Console.ReadKey();
        }
    }
}
