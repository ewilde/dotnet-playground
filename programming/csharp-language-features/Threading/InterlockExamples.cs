using System;
using System.Threading;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;
using log4net;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Threading
{
    public class InterlockExamples
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InterlockExamples));

        public void ExchangeExample()
        {
            const int numberOfTasks = 10;
            var tasks = new Task[numberOfTasks];
            for (int i = 0; i < numberOfTasks; i++)
            {
                tasks[i] = new Task(ExchangeExampleRun, TaskCreationOptions.LongRunning);
            }
            
            tasks.ForEach(task => task.Start());
            wait.Set();
            Task.WaitAll(tasks);
        }

        private int processing = 0;
        private ManualResetEventSlim wait = new ManualResetEventSlim(false);
        public void ExchangeExampleRun()
        {
            wait.Wait();
            Log.DebugFormat("Entry {0}.", processing);

            if (Interlocked.CompareExchange(ref processing, 1, 0) == 1) // Interlocked.CompareExchange returns the original value of processing before the swap
            {
                Log.DebugFormat("Skipping {0}.", processing);
                return;
            }

            Log.DebugFormat("Executing {0}.", processing);

            Interlocked.CompareExchange(ref processing, 0, 1);
            Log.DebugFormat("Reset {0}.", processing);
        }
    }
    
    [TestFixture]
    public class InterlockExamplesTests
    {
        [Test]
        public void prevent_multiple_threads_accessing_a_method()
        {
            TimeSpan x = default(TimeSpan);
            Console.WriteLine(x);
            new InterlockExamples().ExchangeExample();
        }
    }
}