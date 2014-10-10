using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions
{
    /// <summary>
    /// Think of <see cref="IObservable{T}"/> as the producer or writer and 
    /// <see cref="IObserver{T}"/> as the consumer or reader of a seqence of objects
    /// </summary>
    public class Rx_SubjectSimpleExample
    {
        public void Run()
        {
            ConsoleUtility.PrintSuccess("Rx Subject example");
            var topic = new Subject<string>();
            PrintObject(topic);

            topic.OnNext("a");
            topic.OnNext("b");
            topic.OnNext("c");
        }

        public void PrintObject(IObservable<string> stream)
        {
            stream.Subscribe(Console.WriteLine);
        }
    }
}
