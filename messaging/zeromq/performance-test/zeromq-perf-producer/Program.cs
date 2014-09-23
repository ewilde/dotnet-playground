using System;
using System.Diagnostics;
using Edward.Wilde.CSharp.Features.Strings;
using Edward.Wilde.CSharp.Features.Utilities;
using NetMQ;

namespace zeromq.perf.producer
{
    class Program
    {
        static readonly string Message = StringExtensions.RandomString(4.Kilobytes());

        static void Main(string[] args)
        {
            const string address = "ipc://127.0.0.1:5556";
            Console.WriteLine("About to connect to " + address);
            using (var context = NetMQContext.Create())
            using (var publisher = context.CreatePublisherSocket())
            {
                publisher.Bind(address);                                   
                int sendMessages = 0;
                var watch = new Stopwatch();
                watch.Start();
                while (true)
                {
                    publisher.Send(Message, dontWait: true); // by default subscribers match on begining of message data i.e. key:data, or you can use message envelopes
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
