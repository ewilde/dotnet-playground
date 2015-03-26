using System;
using System.IO;
using System.Linq;
using System.Text;
using Edward.Wilde.CSharp.Features.Strings;
using Edward.Wilde.CSharp.Features.Utilities;
using Edward.Wilde.CSharp.Features.Utilities.CityIndex.Build.Model.Utility;
using NUnit.Framework;
using PerformanceMeasurement;
using QuickGraph;
using QuickGraph.Algorithms;

namespace algorithms.Puzzles
{
    /// <summary>
    /// From: https://projecteuler.net/problem=79
    /// 
    /// A common security method used for online banking is to ask the user for three random characters from a passcode. 
    /// For example, if the passcode was 531278, they may ask for the 2nd, 3rd, and 5th characters; 
    /// the expected reply would be: 317.
    /// 
    /// The text file, keylog.txt, contains fifty successful login attempts.
    /// 
    /// Given that the three characters are always asked for in order, 
    /// analyse the file so as to determine the shortest possible secret 
    /// passcode of unknown length.
    /// </summary>
    public class PasscodeDerivation
    {
        public const string KeyLog = @"319
680
180
690
129
620
762
689
762
318
368
710
720
710
629
168
160
689
716
731
736
729
316
729
729
710
769
290
719
680
318
389
162
289
162
718
729
319
790
680
890
362
319
760
316
729
380
319
728
716";

        /// <summary>
        /// Time complexity n + logn?
        /// http://bigocheatsheet.com/
        /// 
        /// Used a directed graph acyclic
        /// http://en.wikipedia.org/wiki/Directed_acyclic_graph
        /// 
        /// The use topological sorting to determine order
        /// https://quickgraph.codeplex.com/wikipage?title=Topological%20Sort
        /// http://en.wikipedia.org/wiki/Topological_sorting#Algorithms
        /// </summary>
        /// <param name="logins"></param>
        /// <returns></returns>
        public static int ShortestPhrase(string[] logins)
        {
            var graph = new AdjacencyGraph<int, SEdge<int>>();

            foreach (var item in logins)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    
                    var number = Convert.ToInt32(item.Substring(i, 1));
                    if (!graph.ContainsVertex(number))
                    {
                        graph.AddVertex(number);
                    }

                    if (i + 1 == item.Length)
                        break;
                    
                    var next = Convert.ToInt32(item.Substring(i + 1, 1));

                    if (!graph.ContainsEdge(number, next))
                    {                        
                        graph.AddEdge(new SEdge<int>(number, next));
                    }

                }
            }

            var shortestPath = graph.TopologicalSort(); // O(log(n))
            var builder = new StringBuilder();
            shortestPath.ForEach(item => builder.Append(item));
            
            return Convert.ToInt32(builder.ToString());
        }
    }

    [TestFixture]
    public class PasscodeDerivationTests
    {
        [Test]
        public void two_iterations()
        {
            var logins = new string[] {"680", "180"};

            Assert.That(PasscodeDerivation.ShortestPhrase(logins), Is.EqualTo(1680));
        }

        [Test]
        public void key_log_txt()
        {
            var logins = PasscodeDerivation.KeyLog.Lines();

            Assert.That(PasscodeDerivation.ShortestPhrase(logins), Is.EqualTo(73162890));
        }

        [Test]
        public void speed_of_alogithm()
        {
            var logins = PasscodeDerivation.KeyLog.Lines();

            StatsCollection result = LinqPadUX.Measure.Action(() => PasscodeDerivation.ShortestPhrase(logins));

            var builder = new StringBuilder();
            
            result.WriteReportTable(new StringWriter(builder), 1f);

            var temporaryFile = FileUtility.GetTemporaryFile(".html");
            File.WriteAllText(temporaryFile,builder.ToString());
            System.Diagnostics.Process.Start(temporaryFile);
            //File.Delete(temporaryFile);
        }
    }
}