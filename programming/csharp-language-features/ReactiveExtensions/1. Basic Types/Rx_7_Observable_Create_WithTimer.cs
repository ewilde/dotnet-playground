using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions
{
    public class Rx_7_Observable_Create_WithTimer
    {

        /// <summary>
        /// This is not the prefered way of working with timers.
        /// </summary>
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Observable create with timer example"));

            var topic = Observable.Create<Price>(consumer =>
            {
                var timer = new System.Timers.Timer();
                timer.Elapsed += (sender, args) => consumer.OnNext(CreatePrice());
                timer.Interval = TimeSpan.FromMilliseconds(10).TotalMilliseconds;
                timer.Start();
                return Disposable.Create(()=>
                {
                    timer.Stop();
                    Console.WriteLine("consumer unsubscribing will shutdown timer.");
                });
            });

            using (var subscripiton = topic.Subscribe(price => Console.WriteLine(price.ToString())))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
        }

        private Random random = new Random();
        private Price CreatePrice()
        {
            var tick = random.Next(10000, 20000) / 100;
            return new Price(10, tick,tick + 1,tick + 2);
        }
    }
}
