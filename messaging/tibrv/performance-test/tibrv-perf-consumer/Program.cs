using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TIBCO.Rendezvous;

namespace tibrv_perf_consumer
{
    class Program
    {
        private static int receivedMessages;
        private static Timer timer;

        private static void Main(string[] args)
        {
           new Program().Run();
            Console.WriteLine("Waiting for messages....");
            Console.ReadKey();
        }

        public void Run()
        {
            Task.Factory.StartNew(Dequeue);
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            
            var service = "7500";
            var network = ";239.255.0.1";
            var daemon = "tcp:7500";
            var subject = "TEST.Perf";
            receivedMessages = 0;

            TIBCO.Rendezvous.Environment.Open();
            var transport = new NetTransport(service, network, daemon);
            var listener = new Listener(Queue.Default, OnMessageReceived, transport, subject, null);
            var dispacher = new Dispatcher(listener.Queue);       
            dispacher.Join();
        }

        private BlockingCollection<MessageReceivedEventArgs> queue = new BlockingCollection<MessageReceivedEventArgs>();
 
        private void OnMessageReceived(object listener, MessageReceivedEventArgs args)
        {
            queue.Add(args);
        }

        private void Dequeue()
        {
            while (true)
            {
                var item = queue.Take();
                Task.Factory.StartNew(() => ProcessMessage(item));
            }
        }

        private static void ProcessMessage(MessageReceivedEventArgs args)
        {
            var body = args.Message.GetField("body").Value as byte[];
            var message = Encoding.UTF8.GetString(body);
            receivedMessages += 1;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("{0} messages per second received. {1} queue size.", receivedMessages, queue.Count);
            receivedMessages = 0;
        }
    }
}
