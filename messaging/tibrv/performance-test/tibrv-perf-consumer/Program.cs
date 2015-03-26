using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Microsoft.Owin.Hosting;
using Reporting.Minion;
using TIBCO.Rendezvous;
using Environment = TIBCO.Rendezvous.Environment;
using Timer = System.Timers.Timer;

namespace tibrv_perf_consumer
{
    class Program
    {
        private static ILog log = LogManager.GetLogger(typeof(Program));
            
        private static int receivedMessages;
        private static Timer timer;

        private static void Main(string[] args)
        {
           new Program().Run();           
        }

        private ManualResetEventSlim eventHandle;

        public Program()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.GetFullPath(@"tibrv-perf-consumer.exe.config")));
            this.eventHandle = new ManualResetEventSlim(initialState: false);
        }

        public void Run()
        {
            string baseAddress = "http://localhost:9000/"; 

            Minion.Start += Start;
            Minion.Stop += Stop;
            
            using (WebApp.Start<Minion>(url: baseAddress))
            {
                log.Info("Minion started waiting for commands");
                this.eventHandle.Wait();
            }
        }

        private void Stop(object sender, EventArgs e)
        {
            log.Info("Stopping test");
            try
            {
                if (dispacher != null) dispacher.Destroy();
                if (listener != null) listener.Destroy();
                if (transport != null) transport.Destroy();
            }
            catch (Exception exception)
            {
                log.Warn("Problem disconnecting from TIBCO", exception);
            }
            this.eventHandle.Set();
        }

        private void Start(object sender, EventArgs eventArgs)
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

            log.InfoFormat("Connecting to TIBCO. Daemon: {0} service: {1}, network: {2}, subject {3}", daemon, service, network, subject);
            try
            {
                Environment.Open();
                this.transport = new NetTransport(service, network, daemon);
                this.listener = new Listener(Queue.Default, OnMessageReceived, transport, subject, null);
                this.dispacher = new Dispatcher(listener.Queue);
                log.Info("Connected to TIBCO successfully");
                log.Info("Waiting for messages");  
                dispacher.Join();
            }
            catch (Exception exception)
            {
                log.Error("Error connecting to TIBCO", exception);
                this.Stop(this, EventArgs.Empty);
            }
        }

        private BlockingCollection<MessageReceivedEventArgs> queue = new BlockingCollection<MessageReceivedEventArgs>();
        private NetTransport transport;
        private Listener listener;
        private Dispatcher dispacher;

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
