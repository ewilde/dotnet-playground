using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.ReactiveExtensions
{
    public class Rx_8_Observable_Range
    {
        public void Run()
        {
            ConsoleUtility.PrintSuccess(string.Format("Observable range example"));

            var range = Observable.Range(1, 10);
            range.Subscribe(Console.WriteLine);
        }
    }
}
