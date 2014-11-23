using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions._3._Inspections
{
    class Rx_inspections_1_any
    {
        public void Any()
        {
            ConsoleUtility.PrintSuccess("Any example");
            var subject = new Subject<int>();
            subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Subject completed"));
            var any = subject.MyAny(i => i > 0);
            any.Subscribe(b => Console.WriteLine("The subject has any values? {0}", b));
            subject.OnNext(1);
            subject.OnCompleted();
        }
    }

    public static class Exercise
    {
        public static IObservable<bool> MyAny<T>(this IObservable<T> sequence, Func<T, bool> predicate)
        {
            var subject = new Subject<bool>();
            var found = false;
            var subscription = sequence.Subscribe(item =>
            {
                if (predicate.Invoke(item))
                {
                    found = true;
                }
            }, () => subject.OnNext(found));

            subject.IgnoreElements().Subscribe(Action., subscription.Dispose);
            return subject;
        }
    }
}
