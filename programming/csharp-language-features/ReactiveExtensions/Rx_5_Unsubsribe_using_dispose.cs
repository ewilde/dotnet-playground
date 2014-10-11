using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions
{
    public class Rx_5_Unsubsribe_using_dispose
    {
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Rx unsubcribe using dispose"));

            var values = new Subject<int>();
            var firstSubscription = values.Subscribe(value =>
            Console.WriteLine("1st subscription received {0}", value));
            var secondSubscription = values.Subscribe(value =>
            Console.WriteLine("2nd subscription received {0}", value));
            values.OnNext(0);
            values.OnNext(1);
            values.OnNext(2);
            values.OnNext(3);
            firstSubscription.Dispose();
            Console.WriteLine("Disposed of 1st subscription");
            values.OnNext(4);
            values.OnNext(5);
        }
    }
}
