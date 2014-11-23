using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions.Sequence_basics
{
    class Rx_seq_2_Distinct
    {
        public void Run_Distinct()
        {
            ConsoleUtility.PrintSuccess(string.Format("Distinct example"));
           
            var subject = new Subject<int>();
            var distinct = subject.Distinct();
            subject.Subscribe(
            i => Console.WriteLine("{0}", i),
            () => Console.WriteLine("subject.OnCompleted()"));
            distinct.Subscribe(
            i => Console.WriteLine("distinct.OnNext({0})", i),
            () => Console.WriteLine("distinct.OnCompleted()"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(3);
            subject.OnNext(3);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnCompleted();
        }

        /// <summary>
        /// Published if previous value differs from current; otherwise skips
        /// </summary>
        public void Run_Distinct_Until_Changed()
        {

            ConsoleUtility.PrintSuccess(string.Format("Distinct until example"));

            var subject = new Subject<int>();
            var distinct = subject.DistinctUntilChanged();
            subject.Subscribe(
            i => Console.WriteLine("{0}", i),
            () => Console.WriteLine("subject.OnCompleted()"));
            distinct.Subscribe(
            i => Console.WriteLine("distinct.OnNext({0})", i),
            () => Console.WriteLine("distinct.OnCompleted()"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(3);
            subject.OnNext(3);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnCompleted();
        }
    }
}
