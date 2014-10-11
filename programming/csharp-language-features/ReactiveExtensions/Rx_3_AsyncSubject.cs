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
    /// <summary>
    /// <see cref="AsyncSubject{T}"/> is similar to replay subject of one (cache size is one), but
    /// will only publish the value once <see cref="AsyncSubject{T}.OnCompleted()"/>
    /// </summary>
    public class Rx_3_AsyncSubject
    {
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Rx AsyncSubject example"));
            var topic = new AsyncSubject<Price>();
            WriteToConsole(topic);

            topic.OnNext(new Price(1, 99, 100, 101));
            topic.OnNext(new Price(1, 98, 99, 100));
            topic.OnNext(new Price(1, 97, 98, 99));

            topic.OnCompleted();
        }

        void WriteToConsole(IObservable<Price> priceStream)
        {
            priceStream.Subscribe(price => Console.WriteLine(price.ToString()));
        }
    }
}
