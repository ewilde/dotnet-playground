using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions
{
    public class Rx_9_Observable_Generate
    {
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Observable generate example"));

            var range = Observable.Generate<int, int>(1, i => i <= 10, i => i + 1, i => i);
            range.Subscribe(Console.WriteLine);
        }

        /// <summary>
        /// Useful for producing dummy data. Publishing at variable rates
        /// </summary>
        public void Run_interval_based()
        {
            ConsoleUtility.PrintSuccess(string.Format("Observable generate with interval example - start"));
            var range = Observable.Generate<Price, Price>(CreatePrice(), price => instanceId < 40,
                price => CreatePrice(), price => price, price => CreateTimeSpance());
            var subscription = range.Subscribe(p => Console.WriteLine(p.ToString()), () => completed.Set());

            completed.WaitOne();
            subscription.Dispose();
            ConsoleUtility.PrintSuccess(string.Format("Observable generate with interval example - end"));
            
        }

        private ManualResetEvent completed = new ManualResetEvent(initialState: false);
        private Random random = new Random();
        private int instanceId = 0;

        private TimeSpan CreateTimeSpance()
        {
            return TimeSpan.FromMilliseconds(random.Next(1, 10
                ));
        }

        private Price CreatePrice()
        {
            var tick = random.Next(10000, 20000) / 100;
            instanceId = instanceId + 1;
            return new Price(10, tick, tick + 1, tick + 2);
        }
    }
}
