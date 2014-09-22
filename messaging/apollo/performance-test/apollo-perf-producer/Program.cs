using System;
using System.Diagnostics;
using Apache.NMS;
using Edward.Wilde.CSharp.Features.Strings;
using Edward.Wilde.CSharp.Features.Utilities;

namespace apollo.perf.producer
{
    class Program
    {
        static readonly string Message = StringExtensions.RandomString(4.Kilobytes());

        static void Main(string[] args)
        {
            var connecturi = new Uri("stomp:tcp://localhost:61613");

            Console.WriteLine("About to connect to " + connecturi);

            var factory = new NMSConnectionFactory(connecturi);
            using (IConnection connection = factory.CreateConnection("admin", "password"))            
            using (var session  = connection.CreateSession())
            {
                connection.Start();                                    
                IDestination destination = session.GetTopic("perf");
                using (IMessageProducer producer = session.CreateProducer(destination))
                {
                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
                    int sendMessages = 0;
                    var watch = new Stopwatch();
                    watch.Start();
                    while (true)
                    {
                        ITextMessage message = session.CreateTextMessage(Message);
                        producer.Send(message);
                        sendMessages += 1;
                        if (watch.ElapsedMilliseconds > 1000)
                        {
                            Console.WriteLine("{0} messages per second sent.", sendMessages);
                            watch.Restart();
                            sendMessages = 0;
                        }
                    }
                }
            }
            
        }
    }
}
