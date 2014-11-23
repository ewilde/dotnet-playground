using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions.Sequence_basics
{
    public class Rx_seq_1_Where
    {
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Sequence where example - start"));
            
            var topic = Observable.Range(0, 10);

            var odds = topic.Where(i => i%2 > 0).Subscribe(i => Console.WriteLine("{0} odd", i));
            var evens = topic.Where(i => i%2 == 0).Subscribe(i => Console.WriteLine("{0} event", i));

            odds.Dispose();
            evens.Dispose();
            ConsoleUtility.PrintSuccess(string.Format("Sequence where example - end"));
            
        }
    }
}
