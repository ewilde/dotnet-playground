using System;
using csharp_language_features;
using Edward.Wilde.CSharp.Features.Sorting;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features
{
    class Program
    {
        static void Main(string[] args)
        {
            new ExpressionTrees().AddTwoNumbersExpression(10, 20);
            
            ConsoleUtility.PrintInfo(string.Empty);

            new SortUsing_IComparable_Example().Run();
            new SortUsing_IComparer_Example().Run();

            Console.ReadKey();

        }
    }
}
