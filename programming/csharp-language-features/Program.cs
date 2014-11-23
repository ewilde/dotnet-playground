using System;

using Edward.Wilde.CSharp.Features.Querying;
using Edward.Wilde.CSharp.Features.Querying.net_3;
using Edward.Wilde.CSharp.Features.ReactiveExtensions;
using Edward.Wilde.CSharp.Features.ReactiveExtensions.Sequence_basics;
using Edward.Wilde.CSharp.Features.ReactiveExtensions._3._Inspections;
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

            new ProductThatCanBeCompared_net2_Example().Run();
            new SortUsing_IComparer_net2_Example().Run();
            new SortUsing_IComparer_net2_delegate_Example().Run();
            new Sorting_in_net3_5().Run();
            new Looping_in_net1().Run();
            new Delegates_in_net2().Run();
            new Linq_method_extensions_in_net3_5().Run();
            new Linq_query_expressions().Run();
            new Linq_to_xml().Run();
            new Linq_sql().Run();

            new Rx_SubjectSimpleExample().Run();
            new Rx_2_ReplaySubject(cacheSize:1).Run();
            new Rx_2_ReplaySubject(cacheSize:10).Run();
            new Rx_3_AsyncSubject().Run();
            new Rx_4_Subscribe_with_error_handling().Run();
            new Rx_5_Unsubsribe_using_dispose().Run();
            new Rx_6_Disposables().Run();
            new Rx_7_Observable_Create().Run();
            new Rx_7_Observable_Create().Run_not_preferred();
            new Rx_7_Observable_Create().Run_empty();
            new Rx_7_Observable_Create().Run_return();
            new Rx_7_Observable_Create().Run_throw();
            new Rx_7_Observable_Create_WithTimer().Run();
            new Rx_8_Observable_Range().Run();
            new Rx_9_Observable_Generate().Run();
            new Rx_9_Observable_Generate().Run_interval_based();
            
            new Rx_seq_1_Where().Run();
            new Rx_seq_2_Distinct().Run_Distinct();
            new Rx_seq_2_Distinct().Run_Distinct_Until_Changed();

            var skipExamples = new Rx_seq_3_Skip_and_Take();
            skipExamples.Skip();
            skipExamples.Take();
            skipExamples.SkipWhile();
            skipExamples.TakeWhile();
            skipExamples.SkipLast();
            skipExamples.TakeLast();

            new Rx_inspections_1_any().Any();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
