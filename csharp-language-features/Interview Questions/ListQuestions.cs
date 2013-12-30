using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_language_features.Interview_Questions
{
    /// <summary>
    /// Questions asked that operate on lists
    /// </summary>
    public static class ListQuestions
    {
        /// <summary>
        /// Question: Reverse the items in the list.
        /// </summary>
        /// <remarks>
        /// The most efficient way to achieve this, is to do it in place.
        /// </remarks>
        public static void Reverse<T>(List<T> list)
        {
            for (int i = 0; i < list.Count / 2; i++)
            {
                T item1 = list[i];
                T item2 = list[list.Count - 1 - i];

                list[i] = item2;
                list[list.Count - 1 - i] = item1;
            }
        }

        /// <summary>
        /// Question: Assuming input items are in sorted order, i.e. { 1 , 1, 3, 5}
        /// write a function that returns the combined lists in sorted order.
        /// </summary>
        /// <example>
        /// Merge(new[] {1,1,5,6}, new[] {2,7}) returns {1, 1, 2, 5, 6, 7}
        /// </example>
        public static List<T> Merge<T>(List<T> list1, List<T> list2) where T : IComparable<T>
        {
            int rightIndex = 0;
            var result = new List<T>(list1.Count + list2.Count);

            for (int i = 0; i < list1.Count; i++)
            {
                if (rightIndex < list2.Count && list2[rightIndex].CompareTo(list1[i]) < 0)
                {
                    result.Add(list2[rightIndex]);
                    rightIndex = rightIndex + 1;
                    i = i - 1;
                }
                else
                {
                    result.Add(list1[i]);
                }
            }

            for (int i = rightIndex; i < list2.Count; i++)
            {
                result.Add(list2[i]);
            }

            return result;
        }
    }
}
