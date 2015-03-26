﻿using NUnit.Framework;

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

        public static int ShortestPhrase(string[] logins)
        {
            return 0;
        }
    }

    public class PasscodeDerivationTests
    {
        public void two_iterations()
        {
            var logins = new string[] {"680", "180"};

            Assert.That(PasscodeDerivation.ShortestPhrase(logins), Is.EqualTo());
        }
    }
}