using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;

namespace console_withpowerargs
{
    class Program
    {
        static void Main(string[] args)
        {
            Args.InvokeAction<CalculatorProgram>(args);
            Console.ReadKey();
        }
    }
}
