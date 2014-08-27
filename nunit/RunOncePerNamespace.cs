// -----------------------------------------------------------------------
// <copyright file="RunOncePerNamespace.cs">
// Copyright (c) 2014.
// </copyright>
// -----------------------------------------------------------------------
namespace nunit.example
{
    using System.Diagnostics;

    using NUnit.Framework;

    [SetUpFixture]
    public class RunOncePerNamespace
    {
        [SetUp]
        public void Setup()
        {
            Debug.WriteLine("SetUpFixture|SetUp: Setup");
        }

        [TearDown]
        public void TearDown()
        {
            Debug.WriteLine("SetUpFixture|TearDown: TearDown");
        }
    }
}