﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Utilities;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions
{
    public class Rx_7_Observable_Create
    {

        /// <summary>
        /// The prefered method for creating observables is use the <see cref="Observable"/>
        /// class and not the <see cref="Subject{T}"/> classes.
        /// </summary>
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Observable create example"));

            var topic = Observable.Create((IObserver<Price> consumer) =>
            {
                consumer.OnNext(new Price(10, 1, 2, 3));
                return Disposable.Create(() => Console.WriteLine("Consumer has unsubscribed"));
            });

            using (topic.Subscribe(price => Console.WriteLine(price.ToString())))
            {
                
            }

            Console.WriteLine("Finished");
        }

        public void Run_not_preferred()
        {
            ConsoleUtility.PrintSuccess(string.Format("Observable create example - not preferred"));

            var topic = new Subject<Price>();
            var subscription = topic.Subscribe(
                price => Console.WriteLine(price.ToString()));

            topic.OnNext(new Price(10, 1, 2, 3));
            topic.OnCompleted();
            subscription.Dispose();
        }
    }
}
