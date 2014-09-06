using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edward.Wilde.CSharp.Features.Delegates
{
    delegate int CalculateAdd(int a, int b);

    /// <summary>
    /// Delegates types as declared above are special types that inherit from <see cref="MulticastDelegate"/>.
    /// Delegate instances are similar to normal reference type instances and can be passed around in the same way.
    /// </summary>
    class Example_using_delegates
    {
        public void Run()
        {
            var calculatorDelegate = new CalculateAdd(this.Sum);
            Console.WriteLine("Sum result using a delegate: {0}", calculatorDelegate(5,6));

            CalculateAdd anotherDelegate = this.Sum;
            Console.WriteLine(anotherDelegate.Invoke(4, 34));
        }
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
