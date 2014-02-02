using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edward.Wilde.CSharp.Features.Delegates
{
    internal delegate int CalculateAdd(int a, int b);

    class Example_using_delegates
    {
        public void Run()
        {
            
        }
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
