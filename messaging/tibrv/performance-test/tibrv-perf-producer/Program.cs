using System;
using System.Diagnostics;
using Edward.Wilde.CSharp.Features.Strings;
using Edward.Wilde.CSharp.Features.Utilities;
using TIBCO.Rendezvous;

namespace tibrv.perf.producer
{
    class Program
    {
        static readonly byte[] messageBody = System.Text.Encoding.UTF8.GetBytes(StringExtensions.RandomString(4.Kilobytes()));

        static void Main(string[] args)
        {
            var service = "7500";
            var network = ";239.255.0.1";
            var daemon = "tcp:7500";
            var subject = "TEST.Perf";
            var sendMessages = 0;

            TIBCO.Rendezvous.Environment.Open();
            var transport = new NetTransport(service, network, daemon);
            var watch = new Stopwatch();
            watch.Start();
            
            while (true)
            {
                var message = new Message { SendSubject = subject };
                message.AddField("body", messageBody);
                transport.Send(message);

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
