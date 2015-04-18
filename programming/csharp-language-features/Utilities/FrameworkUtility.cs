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

    [TestFixture]
    public class FrameworkUtilityTests
    {
        [Test]
        public void IsNet450OrNewer()
        {
            Assert.That(FrameworkUtility.IsNet45OrNewer(), Is.True);

            "ed".Substring(0, 123);
        }
    }
}