using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions
{
    class Rx_2_ReplaySubject
    {
        private int cacheSize;

        public Rx_2_ReplaySubject(int cacheSize)
        {
            this.cacheSize = cacheSize;
        }
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Rx ReplaySubject example, cache size:{0}", this.cacheSize));
            var priceStream = new ReplaySubject<Price>(bufferSize: this.cacheSize);
            priceStream.OnNext(new Price(10, 101.5, 102, 102.5));
            priceStream.OnNext(new Price(10, 102.5, 103, 103.5));
            priceStream.OnNext(new Price(10, 101.5, 102, 102.5));
            WriteToConsole(priceStream);
        }

        private void WriteToConsole(IObservable<Price> items)
        {
            items.Subscribe(x => Console.WriteLine(x.ToString()));
        }
    }
}
