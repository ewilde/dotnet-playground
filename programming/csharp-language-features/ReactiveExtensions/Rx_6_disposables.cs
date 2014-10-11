using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions
{
    public class Rx_6_Disposables
    {
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Disposable example"));

            var item = Disposable.Create(() => Console.WriteLine("I am disposed"));
            item.Dispose();
            item.Dispose();

        }
    }
}
