// -----------------------------------------------------------------------
// <copyright file="ProductTests.cs">
// Copyright (c) 2014.
// </copyright>
// -----------------------------------------------------------------------
namespace nunit.example
{
    using System.Diagnostics;

    using Edward.Wilde.CSharp.Features.Model;

    using NUnit.Framework;


    [TestFixture]
    public class SampleTests
    {
        [TestFixtureSetUp] // Once per class
        public void TestFixtureSetUp()
        {
            Debug.WriteLine("SampleTests: TestFixtureSetUp");            
        }

        [TestFixtureTearDown] // Once per class
        public void TestFixtureTearDown()
        {
            Debug.WriteLine("SampleTests: TestFixtureTearDown");            
        }

        [SetUp] // Once per test
        public void SetUp()
        {
            Debug.WriteLine("SampleTests: Setup");
        }

        [TearDown] // Once per test
        public void TearDown()
        {
            Debug.WriteLine("SampleTests: TearDown");
        }
    
        [Test]
        public void creating_an_instance_of_employee_should_initialize_foo()
        {
            var product = new Product("Bean", 15.5m);
            Assert.That(product.Name, Is.EqualTo("Bean"));
        }

        [Test]
        public void creating_an_instance_of_employee_should_initialize_foo2()
        {
            var product = new Product("Bean", 15.5m);
            Assert.That(product.Name, Is.EqualTo("Bean"));
        }
    }
}