using System;
using System.Diagnostics;
using System.Text;
using Edward.Wilde.CSharp.Features.Strings;
using Edward.Wilde.CSharp.Features.Utilities;
using RabbitMQ.Client;

namespace rabbitmq.perf.producer
{
    class Program
    {
        static readonly byte[] Message = System.Text.Encoding.UTF8.GetBytes(StringExtensions.RandomString(4.Kilobytes()));
                
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            int sendMessages = 0;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "perf", type: "fanout", durable: false);

                Console.WriteLine(" [*] Performing text." + "To exit press CTRL+C");

                var watch = new Stopwatch();
                watch.Start();
                while (true)
                {
                    channel.BasicPublish("perf", "", null, Message);
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
