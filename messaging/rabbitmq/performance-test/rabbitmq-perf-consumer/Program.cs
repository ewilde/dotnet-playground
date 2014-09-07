using System;
using System.Diagnostics;
using System.Text;
using System.Timers;
using Edward.Wilde.CSharp.Features.Strings;
using Edward.Wilde.CSharp.Features.Utilities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace rabbitmq.perf.consumer
{
    class Program
    {
        private static int receivedMessages = 0;
        private static Timer timer;

        static void Main(string[] args)
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed +=timer_Elapsed;
            new Program().Run();
        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("{0} messages per second received.", receivedMessages);
                receivedMessages = 0;            
        }

        public void Run()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "perf", type: "fanout");

                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName, exchange: "perf", routingKey: "");
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, true, consumer);

                    Console.WriteLine(" [*] Waiting for perf. To exit press CTRL+C");
                    timer.Start();
                    while (true)
                    {
                        var ea = consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        receivedMessages += 1;
                    }
                }
            }
        }        
    }
}
