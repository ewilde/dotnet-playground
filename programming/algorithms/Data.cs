using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace algorithms
{
    public class Data
    {
        public static int[] GetSortedInteger(int numberOfItems, bool @ascending = true)
        {
            return Unfold(@ascending ? 1 : numberOfItems, i => @ascending ? i + 1 : i - 1).Take(numberOfItems).ToArray();
        }

        private static IEnumerable<T> Unfold<T>(T seed, Func<T, T> accumulator)
        {
            var nextValue = seed;
            while (true)
            {
                yield return nextValue;
                nextValue = accumulator(nextValue);
            }
        }
    }
}