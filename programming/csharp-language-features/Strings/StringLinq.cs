using System;
using System.Linq;

namespace Edward.Wilde.CSharp.Features.Strings
{
    public class StringLinq
    {
        public void Concatatention_example_warning_not_performant_obviously()
        {
            string[] words = { "one", "two", "three" };
            var res = words.Aggregate((current, next) => current + ", " + next);
            Console.WriteLine(res);
        }
    }
}