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
        [TestFixtureSetUp]
        public void Setup()
        {
            Debug.WriteLine("TestFixture|TestFixtureSetUp: Setup");            
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Debug.WriteLine("TestFixture|TestFixtureSetUp: TearDown");            
        }

    
        [Test]
        public void creating_an_instance_of_employee_should_initialize_foo()
        {
            var product = new Product("Bean", 15.5m);
            Assert.That(product.Name, Is.EqualTo("Bean"));
        }
    }
}