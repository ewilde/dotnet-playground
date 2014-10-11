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
    public class Rx_4_Subscribe_with_error_handling
    {
        public void Run()
        {
            var topic = new Subject<Price>();

            topic.Subscribe(price => Console.WriteLine(price.ToString()),
                exception => ConsoleUtility.PrintError(exception.ToString()));
                            
            topic.OnNext(new Price(1, 99, 100, 101));
            topic.OnNext(new Price(1, 98, 99, 100));
            topic.OnNext(new Price(1, 97, 98, 99));
            topic.OnError(new Exception("Price feed unavailable...")); 
            topic.OnNext(new Price(1, 98, 99, 100)); // Won't publish as stream completes OnError
        }
    }
}
