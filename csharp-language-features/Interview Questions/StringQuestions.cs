using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace csharp_language_features.InterviewQuestions
{
    public static class StringsQuestions
    {
        #region Question 1: Implement an algorithm to determine if a string has all unique characters

        /// <Summary>
        /// Question 1:
        /// Implement an algorithm to determine if a string has all unique characters. What if you can not use additional data structures? 
        /// </Summary>
        public static bool AreAllCharactersUnique(string values)
        {
            foreach (var character in values)
            {
                if (!AreAllCharactersUnique(character, values))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AreAllCharactersUnique(char character, string value)
        {
            int count = 0;
            foreach (var item in value)
            {
                if (item.Equals(character))
                {
                    count = count + 1;
                    if (count > 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <Summary>
        /// Question 1:
        /// Implement an algorithm to determine if a string has all unique characters. What if you can not use additional data structures other than Arrays? 
        /// </Summary>
        /// <remarks>
        /// Assuming all strings are in the ascii range.
        /// </remarks>
        public static bool AreAllCharactersUnique2(string value)
        {
            bool[] found = new bool[256];

            foreach (var character in value)
            {
                if (found[character])
                {
                    return false;
                }

                found[character] = true;
            }

            return true;
        }

        #region tests

        public class StringQuestionTests
        {
            [Test]
            public void All_unique_characters_return_true()
            {
                Assert.That(AreAllCharactersUnique("Bed"), Is.True);
            }

            [Test]
            public void Not_unique_returns_false()
            {
                Assert.That(AreAllCharactersUnique("Beed"), Is.False);
            }

            [Test]
            public void Empty_string_returns_true()
            {
                Assert.That(AreAllCharactersUnique(string.Empty), Is.True);
            }

        }

        public class StringQuestionTests2
        {
            [Test]
            public void All_unique_characters_return_true()
            {
                Assert.That(AreAllCharactersUnique2("Bed"), Is.True);
            }

            [Test]
            public void Not_unique_returns_false()
            {
                Assert.That(AreAllCharactersUnique2("Beed"), Is.False);
            }

            [Test]
            public void Empty_string_returns_true()
            {
                Assert.That(AreAllCharactersUnique2(string.Empty), Is.True);
            }

        }

        #endregion

        #endregion

        #region Question 2: Design an algorithm and write code to remove the duplicate characters in a string
        public static char[] RemoveDuplicates(char[] str)
        {
            if (str == null) return str;
            int len = str.Length;
            if (len < 2) return str;

            var found = new bool[256];

            int tail = 1;
            for (int i = 1; i < str.Length; i++)
            {
                char x = str[i];
                if (tail != i)
                {
                    str[tail] = str[i];
                    str[i] = (char)0;
                }

                if (!found[x])
                {
                    found[x] = true;
                    tail = tail + 1;
                }
                else
                {
                    
                    str[tail] = (char)0; 
                }                
            }


            return str;
        }

        #region Tests

        [TestFixture]
        public class Question2Tests
        {
            [Test]
            public void Tests()
            {
                Assert.That(RemoveDuplicates("Eddie".ToCharArray()), Is.EquivalentTo(new List<char>("Edie".ToCharArray()) { (char)0}));
                Assert.That(RemoveDuplicates("Edddd".ToCharArray()), Is.EquivalentTo(new List<char>("Ed".ToCharArray()) { (char)0, (char)0, (char)0 }));

                Assert.That(RemoveDuplicates("Edi".ToCharArray()), Is.EquivalentTo(new List<char>("Edi".ToCharArray()) { }));
            }
        }
        #endregion

        #endregion

        #region Question 3: Write a method to decide if two strings are anagrams or not.

        /// <summary>
        /// This solutions only works for ascii values.
        /// </summary>
        /// <remarks>2n time complexity</remarks>
        public static bool Anagram1(string valueA, string valueB)
        {
            if (string.IsNullOrWhiteSpace(valueA) || string.IsNullOrWhiteSpace(valueB))
            {
                return false;
            }

            if (valueA.Length != valueB.Length)
            {
                return false;
            }

            if (valueA.Equals(valueB))
            {
                return false;
            }

            var occuranceCountA = new Int16[256];
            var occuranceCountB = new Int16[256];
            for (var index = 0; index < valueA.Length; index++)
            {
                occuranceCountA[valueA[index]] += 1;
                occuranceCountB[valueB[index]] += 1;
            }
            
            for (var i = 0; i < occuranceCountA.Length; i++)
            {
                if (occuranceCountA[i] != occuranceCountB[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// This solutions also only works for ascii values and uses 1/2 the amount of memory.
        /// </summary>
        /// <remarks>2n time complexity</remarks>
        public static bool Anagram2(string valueA, string valueB)
        {
            if (string.IsNullOrWhiteSpace(valueA) || string.IsNullOrWhiteSpace(valueB))
            {
                return false;
            }

            if (valueA.Length != valueB.Length)
            {
                return false;
            }

            if (valueA.Equals(valueB))
            {
                return false;
            }

            var occuranceCount = new Int16[256];
            int uniqueCharacters = 0;
            for (var i = 0; i < valueA.Length; i++)
            {
                char item = valueA[i];
                if (occuranceCount[item] == 0)
                {
                    uniqueCharacters+=1;
                }

                occuranceCount[item] += 1;
            }

            for (var i = 0; i < valueB.Length; i++)
            {
                char item = valueB[i];
                if (occuranceCount[item] == 0)
                {
                    return false; // too many occurances of char found
                }

                occuranceCount[item] -= 1;
                if (occuranceCount[item] == 0)
                {
                    uniqueCharacters -= 1;

                    if (uniqueCharacters == 0)
                    {
                        return i == valueB.Length - 1;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// This solutions works for all unicode string values and uses a dictionary.
        /// </summary>
        /// <remarks>2n time complexity. Dictionary insertion and retrieval will be minimal</remarks>
        public static bool Anagram3(string valueA, string valueB)
        {
            if (string.IsNullOrWhiteSpace(valueA) || string.IsNullOrWhiteSpace(valueB))
            {
                return false;
            }

            if (valueA.Length != valueB.Length)
            {
                return false;
            }

            if (valueA.Equals(valueB))
            {
                return false;
            }

            var values = new Dictionary<char, int>();
            foreach (char key in valueA)
            {
                if (!values.ContainsKey(key))
                {
                    values[key] = 1;
                }
                else
                {
                    values[key] += 1;
                }
            }

            var uniqueCharacters = values.Keys.Count;
            for (var i = 0; i < valueB.Length; i++)
            {
                char item = valueB[i];
                if (!values.ContainsKey(item) || values[item] == 0)
                {
                    return false; // too many occurances of char found
                }

                values[item] -= 1;
                if (values[item] == 0)
                {
                    uniqueCharacters -= 1;

                    if (uniqueCharacters == 0)
                    {
                        return i == valueB.Length - 1;
                    }
                }
            }

            return false;
        }

        #region Tests
        [TestFixture]
        public class AnagramTests
        {
            [Test]
            public void Anagram1Test()
            {
                Assert.That(Anagram1(null, null), Is.False);
                Assert.That(Anagram1(string.Empty, null), Is.False);
                Assert.That(Anagram1("a", null), Is.False);

                Assert.That(Anagram1("ab", "ab"), Is.False);
                Assert.That(Anagram1("carthorses", "orchestra"), Is.False);
                Assert.That(Anagram1("orchestra", "carthorses"), Is.False);
                Assert.That(Anagram1("abc", "bce"), Is.False);

                Assert.That(Anagram1("orchestra", "carthorse"), Is.True);
            }

            /// <summary>
            /// Anagram2s the test.
            /// </summary>
            [Test]
            public void Anagram2Test()
            {
                Assert.That(Anagram2(null, null), Is.False);
                Assert.That(Anagram2(string.Empty, null), Is.False);
                Assert.That(Anagram2("a", null), Is.False);

                Assert.That(Anagram2("ab", "ab"), Is.False);
                Assert.That(Anagram2("carthorses", "orchestra"), Is.False);
                Assert.That(Anagram2("orchestra", "carthorses"), Is.False);
                Assert.That(Anagram2("abc", "bce"), Is.False);

                Assert.That(Anagram2("orchestra", "carthorse"), Is.True);
            }

            /// <summary>
            /// Anagram3s the test.
            /// </summary>
            [Test]
            public void Anagram3Test()
            {
                Assert.That(Anagram3(null, null), Is.False);
                Assert.That(Anagram3(string.Empty, null), Is.False);
                Assert.That(Anagram3("a", null), Is.False);

                Assert.That(Anagram3("ab", "ab"), Is.False);
                Assert.That(Anagram3("carthorses", "orchestra"), Is.False);
                Assert.That(Anagram3("orchestra", "carthorses"), Is.False);
                Assert.That(Anagram3("abc", "bce"), Is.False);

                Assert.That(Anagram3("orchestra", "carthorse"), Is.True);
            }
        }
        #endregion

        #endregion

    }
}