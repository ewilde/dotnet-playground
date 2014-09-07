using System;
using System.CodeDom;
using System.Diagnostics;
using Apache.NMS;
using Edward.Wilde.CSharp.Features.Strings;
using Edward.Wilde.CSharp.Features.Utilities;

namespace activemq.perf.producer
{
    class Program
    {
        static readonly string Message = StringExtensions.RandomString(4.Kilobytes());

        static void Main(string[] args)
        {
            var connecturi = new Uri("activemq:tcp://localhost:61616");

            Console.WriteLine("About to connect to " + connecturi);

            var factory = new Apache.NMS.ActiveMQ.ConnectionFactory(connecturi);
            using (var connection = factory.CreateConnection())
            using (var session  = connection.CreateSession())
            {
                IDestination destination = session.GetTopic("perf");
                using (IMessageProducer producer = session.CreateProducer(destination))
                {
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
