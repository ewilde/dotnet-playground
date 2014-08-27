using System;
using System.Diagnostics;
using Edward.Wilde.CSharp.Features.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mstest_unittest_example
{
    [TestClass]
    public class SampleTests
    {
        [ClassInitialize] // Once per class
        public static void TestFixtureSetUp(TestContext context)
        {
            Debug.WriteLine("SampleTests: TestFixtureSetUp");
        }

        [ClassCleanup] // Once per class
        public static void TestFixtureTearDown()
        {
            Debug.WriteLine("SampleTests: TestFixtureTearDown");
        }

        [TestInitialize] // Once per test
        public void SetUp()
        {
            Debug.WriteLine("SampleTests: Setup");
        }

        [TestCleanup] // Once per test
        public void TearDown()
        {
            Debug.WriteLine("SampleTests: TearDown");
        }

        [TestMethod]
        public void creating_an_instance_of_employee_should_initialize_foo()
        {
            var product = new Product("Bean", 15.5m);
            product.Name.Should().Be("Bean");
        }

        [TestMethod]
        public void creating_an_instance_of_employee_should_initialize_foo2()
        {
            var product = new Product("Bean", 15.5m);
            product.Name.Should().Be("Bean");
        }
    }
}
