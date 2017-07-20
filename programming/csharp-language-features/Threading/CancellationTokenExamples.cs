using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Threading
{
    public class CancellationTokenExamples
    {
        private CancellationTokenSource source;
        private long startTicks;

        public TimeSpan ElapsedTime { get; private set; }

        public void Start()
        {
            startTicks = DateTime.Now.Ticks;
			source = new CancellationTokenSource();
            var token = source.Token;

            var task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested) {
                    Console.WriteLine("Working on my task....");
                    Thread.Sleep((int)TimeSpan.FromSeconds(1).TotalMilliseconds);
                }
            });
        }

        public void Stop()
        {
            source.Cancel();
            ElapsedTime = new TimeSpan(DateTime.Now.Ticks - startTicks);
        }
    }

    [TestFixture]
    public class CancellationTokenExamplesTests
    {
        [Test]
        public void RunATaskUntilCancelled()
        {
            var tokenExample = new CancellationTokenExamples();

            tokenExample.Start();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            tokenExample.Stop();

            Assert.That(tokenExample.ElapsedTime, Is.GreaterThan(TimeSpan.FromSeconds(1)));
        }
    }
}
