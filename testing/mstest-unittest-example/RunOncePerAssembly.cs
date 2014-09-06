// -----------------------------------------------------------------------
// <copyright file="RunOncePerAssembly.cs">
// Copyright (c) 2014.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mstest_unittest_example
{
    using System.Diagnostics;

    [TestClass]
    public class RunOncePerAssembly
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            Debug.WriteLine("RunOncePerAssembly: AssemblyInitialize");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Debug.WriteLine("RunOncePerAssembly: AssemblyCleanup");
        }
    }
}