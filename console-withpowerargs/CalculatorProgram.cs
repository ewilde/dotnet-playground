using System;
using PowerArgs;

namespace console_withpowerargs
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    public class CalculatorProgram
    {
        [HelpHook, ArgShortcut("-?"), ArgDescription("Shows this help")]
        public bool Help { get; set; }

        [ArgActionMethod, ArgDescription("Adds the two operands")]
        public void Add(TwoOperandArgs args)
        {
            Console.WriteLine(args.Value1 + args.Value2);
        }       
    }
}