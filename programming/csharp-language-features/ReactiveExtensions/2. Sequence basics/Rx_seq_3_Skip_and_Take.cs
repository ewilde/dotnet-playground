using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions.Sequence_basics
{
    class Rx_seq_3_Skip_and_Take
    {
        public void Skip()
        {
            ConsoleUtility.PrintSuccess(string.Format("Skip example"));

            var sequence = Observable.Range(0, 10);
            sequence.Skip(2).Subscribe(Console.WriteLine); // Skip first two
        }

        public void Take()
        {
            ConsoleUtility.PrintSuccess(string.Format("Take example"));

            var sequence = Observable.Range(0, 10);
            sequence.Take(2).Subscribe(Console.WriteLine); // Take first two
        }

        public void SkipWhile()
        {
            ConsoleUtility.PrintSuccess(string.Format("SkipWhile example"));
            var subject = new Subject<int>();
            subject
            .SkipWhile(i => i < 4)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(3);
            subject.OnNext(3);
            subject.OnNext(4); // Skips until this line
            subject.OnNext(3);
            subject.OnNext(2);
            subject.OnNext(1);
            subject.OnNext(0);
            subject.OnCompleted();
        }

        public void TakeWhile()
        {
            ConsoleUtility.PrintSuccess(string.Format("TakeWhile example"));
            var subject = new Subject<int>();
            subject
            .SkipWhile(i => i > 4)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(3);
            subject.OnNext(3);
            subject.OnNext(5); // Takes until this line
            subject.OnNext(3);
            subject.OnNext(2);
            subject.OnNext(1);
            subject.OnNext(0);
            subject.OnCompleted();
        }

        public void SkipLast()
        {
            ConsoleUtility.PrintSuccess(string.Format("SkipLast n example"));
            var seq = Observable.Range(0, 10);
            seq.SkipLast(2).Subscribe(Console.WriteLine); // Skips last 2 elements
        }

        public void TakeLast()
        {
            ConsoleUtility.PrintSuccess(string.Format("TakeLast n example"));
            var seq = Observable.Range(0, 10);
            seq.TakeLast(2).Subscribe(Console.WriteLine); // Take last 2 elements
        }

        /// <summary>
        /// Skips until the other seq publishes an item (Unit.Default)
        /// </summary>
        public void SkipUntil()
        {
            var subject = new Subject<int>();
            var otherSubject = new Subject<Unit>();
            subject
            .SkipUntil(otherSubject)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            otherSubject.OnNext(Unit.Default);
            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);
            subject.OnNext(7);
            subject.OnNext(8);
            subject.OnCompleted();
        }

        public void TakeUntil()
        {
            var subject = new Subject<int>();
            var otherSubject = new Subject<Unit>();
            subject
            .TakeUntil(otherSubject)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            otherSubject.OnNext(Unit.Default);
            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);
            subject.OnNext(7);
            subject.OnNext(8);
            subject.OnCompleted();
        }
    }
}
