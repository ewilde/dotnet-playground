using System;
using System.Timers;
using Apache.NMS;

namespace apollo.perf.consumer
{
    class Program
    {
        private static int receivedMessages = 0;
        private static Timer timer;

        static void Main(string[] args)
        {
            var connecturi = new Uri("stomp:tcp://localhost:61613");
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;

            Console.WriteLine("About to connect to " + connecturi);

            var factory = new NMSConnectionFactory(connecturi);
            using (var connection = factory.CreateConnection("admin", "password"))
            using (var session = connection.CreateSession())
            {
                connection.Start();
                IDestination destination = session.GetTopic("perf");
                using (IMessageConsumer consumer = session.CreateConsumer(destination))
                {
                    while (true)
                    {
                        timer.Start();
                        var message = consumer.Receive();
                        receivedMessages += 1;
                        //Console.WriteLine(message);
                    }
                }
            }
        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("{0} messages per second received.", receivedMessages);
            receivedMessages = 0;
        }
    }
}
