using System;
using System.Linq;
using NUnit.Framework;

namespace algorithms.Strings
{
    public class WordsAndCharacters
    {
        public bool IsUnique(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            var data = new bool[127];
            for (var i = 0; i < value.Length; i++)
            {
                if (data[value[i]])
                {
                    return false;
                }

                data[value[i]] = true;
            }

            return true;
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("abc", true)]
        [TestCase("abcbe", false)]
        [TestCase("Aa", true)]
        public void TestIsUnique(string value, bool expectedResult)
        {
            Assert.That(IsUnique(value), Is.EqualTo(expectedResult));
        }

        public string Reverse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var result = new char[value.Length];
            for (var i = value.Length - 1; i >= 0; i--)
            {
                result[i] = value[value.Length - (i + 1)];
            }

            return new string(result);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("a", "a")]
        [TestCase("ba", "ab")]
        [TestCase("abc", "cba")]
        [TestCase("abcdefghi", "ihgfedcba")]
        public void TestReverse(string input, string output)
        {
            Assert.That(Reverse(input), Is.EqualTo(output));
        }

        public string Reverse2(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length == 1)
                return value;

            var result = new char[value.Length];
            for (var tail = ((value.Length - 1) / 2) + 1; tail >= 0; tail--)
            {
                var head = value.Length - 1 - tail;
                var headValue = value[head];
                result[tail] = headValue;
                result[head] = value[tail];
            }

            return new string(result);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("a", "a")]
        [TestCase("ba", "ab")]
        [TestCase("abc", "cba")]
        [TestCase("abcdefghi", "ihgfedcba")]
        public void TestReverse2(string input, string output)
        {
            Assert.That(Reverse2(input), Is.EqualTo(output));            
        }

        public char[] Reverse(char[] values)
        {
            if (values == null || values.Length == 0)
                return values;

            for (var i = 0; i < values.Length -1; i++)
            {
                var source = values[values.Length - 1 - i];
                values[values.Length - 1 - i] = values[i];
                values[i] = source;
            }

            return values;
        }

        [TestCase(null, null)]
        [TestCase(new char[] {}, new char[] { })]
        [TestCase(new char[] { 'a' }, new char[] { 'a' })]
        [TestCase(new char[] { 'a', 'b' }, new char[] { 'b', 'a' })]
        [TestCase(new char[] { 'a', 'b', 'c' }, new char[] { 'c', 'b', 'a' })]
        public void TestReverse(char[] input, char[] output)
        {
            Assert.That(Reverse(input), Is.EqualTo(output));
        }

        /// <summary>
        /// Remove duplicate characters without using a second datastructure
        /// </summary>
        public char[] RemoveDuplicates(char[] values)
        {
            if (values == null || values.Length == 0)
                return values;

            var copyTo = 0;
            var numberOfReplacements = 0;
            var lastChar = values[0];

            for (var i = 1; i < values.Length; i ++)
            {
                if (values[i] == lastChar)
                {
                    copyTo = i;
                    numberOfReplacements += 1;
                    continue;
                }

                if (copyTo > 0)
                {
                    values[copyTo] = values[i];
                    copyTo += 1;
                }
            }

            Array.Resize(ref values, values.Length - numberOfReplacements);
            return values;
        }

        /// <summary>
        /// Remove duplicate characters without using a second datastructure
        /// </summary>
        public char[] RemoveDuplicates2(char[] values)
        {
            if (values == null || values.Length == 0)
                return values;

            var tail = 1;
            
            for (var i = 1; i < values.Length; i++)
            {
                int j;
                for (j = 0; j < tail; j++)
                {
                    if (values[i] == values[j])
                        break;    
                }

                if (j == tail)
                {
                    values[tail] = values[i];
                    tail++;
                }
            }

            Array.Resize(ref values, tail);
            return values;
        }

        [TestCase(null, null)]
        [TestCase(new char[] { }, new char[] { })]
        [TestCase(new char[] { 'a' }, new char[] { 'a' })]
        [TestCase(new char[] { 'a', 'b' }, new char[] { 'a', 'b' })]
        [TestCase(new char[] { 'a', 'a', 'b' }, new char[] { 'a', 'b' })]
        public void TestRemoveDuplicates(char[] input, char[] output)
        {
            Assert.That(RemoveDuplicates(input), Is.EqualTo(output));
        }

        [TestCase(new char[] { 'a', 'a', 'b', 'a', 'a','b', 'b' }, new char[] { 'a', 'b' })]
        public void TestRemoveDuplicates2(char[] input, char[] output)
        {
            Assert.That(RemoveDuplicates2(input), Is.EqualTo(output));
        }

        /// <summary>
        /// Time complexity O(n) space complexity O(n)
        /// </summary>
        public char[] RemoveDuplicates3(char[] values)
        {
            if (values == null || values.Length < 2)
                return values;

            var hits = new bool[127];
            var tail = 0;
            hits[values[0]] = true;

            for (var i = 1; i < values.Length; i++)
            {
                if (hits[values[i]])
                    continue;

                hits[values[i]] = true;
                tail++;
                values[tail] = values[i];
            }

            Array.Resize(ref values, tail + 1);
            return values;
        }


        [TestCase(new char[] { 'a', 'a', 'b', 'a', 'a', 'b', 'b' }, new char[] { 'a', 'b' })]
        public void TestRemoveDuplicates3(char[] input, char[] output)
        {
            Assert.That(RemoveDuplicates3(input), Is.EqualTo(output));
        }

        public bool IsAnagram(string word1, string word2)
        {
            var len1 = word1?.Length ?? 0;
            var len2 = word2?.Length ?? 0;

            if (len1 != len2 || len1 == 0 || len2 == 0 )
                return false;

            var chars1 = word1.Select(x => x).OrderBy(x => x).ToArray();
            var chars2 = word2.Select(x => x).OrderBy(x => x).ToArray();

            for (var i = 0; i < chars1.Length; i++)
            {
                if (chars1[i] != chars2[i])
                    return false;
            }

            return true;
        }

        [TestCase(null, null, false)]
        [TestCase("", "", false)]
        [TestCase("abc", "", false)]
        [TestCase("abcbe", "abcba", false)]
        [TestCase("Aa", "aA", true)]
        [TestCase("unitedkingdom", "didmenukingto", true)]
        public void TestIsAnagram(string input1, string input2, bool output)
        {
            Assert.That(IsAnagram(input1, input2), Is.EqualTo(output));
        }

        public string ReplaceCharWithString(char[] values, char replace, string with)
        {
            if (values == null || values.Length == 0)
                return values == null ? null : string.Empty;

            var find = 0;
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] == replace)
                    find++;
            }

            var result = new char[values.Length + ((with.Length - 1) * find)];
            var head = result.Length - 1;
            for (var i = values.Length - 1; i >= 0; i--)
            {
                if (values[i] == replace)
                {
                    for (var j = 0; j < with.Length; j++)
                    {
                        result[head - (with.Length - j - 1)] = with[j];
                    }

                    head -= with.Length;
                }
                else
                {
                    result[head] = values[i];
                    head--;
                }
            }

            return new string(result);
        }

        [TestCase(null, 'a', "Foo", null)]
        [TestCase(new char[] {'a','b', 'c'}, 'a', "Foo", "Foobc")]
        [TestCase(new char[] {'a','b', 'c'}, 'b', "Foo", "aFooc")]
        [TestCase(new char[] {'a','b', 'a'}, 'a', "Foo", "FoobFoo")]
        public void TestReplaceCharWithString(char[] input, char replace, string with, string expectedResult)
        {
            Assert.That(ReplaceCharWithString(input, replace, with), Is.EqualTo(expectedResult));
        }
    }
}