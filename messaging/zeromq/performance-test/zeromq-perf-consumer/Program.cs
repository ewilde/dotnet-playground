using System;
using System.Timers;
using NetMQ;

namespace zeromq.perf.consumer
{
    class Program
    {
        private static int receivedMessages = 0;
        private static Timer timer;

        static void Main(string[] args)
        {
            const string address = "ipc://127.0.0.1:5556";
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;

            Console.WriteLine("About to connect to " + address);

            using (var context = NetMQContext.Create())
            using (var subscriber = context.CreateSubscriberSocket())
            {
                subscriber.Connect(address);
                timer.Start();
                subscriber.Subscribe("");
                while (true)
                {
                    var message = subscriber.ReceiveString();
                    receivedMessages += 1;
                    //Console.WriteLine(message);
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
