using System;

namespace rabbitmq.workqueue.producer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Send().Run(args);
        }
    }
}
