using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace csharp_language_features.Interview_Questions
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

        #region Question 2:
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
    }
}