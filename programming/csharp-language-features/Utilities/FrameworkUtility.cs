using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public static class FrameworkUtility
    {
        public static bool IsNet45OrNewer()
        {
            // Class "ReflectionContext" exists from .NET 4.5 onwards.
            return Type.GetType("System.Reflection.ReflectionContext", false) != null;
        }        
    }

    public class FrameworkUtilityTests
    {
        [Test]
        public void IsNet450OrNewer()
        {
            Assert.That(FrameworkUtility.IsNet45OrNewer(), Is.True);

            const int MaxMarketId = 7;
            for (int i = 0 ; i < 100; i++)
            {
                Debug.WriteLine(i%MaxMarketId + 1);
            }
        }
    }
}