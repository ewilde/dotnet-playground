using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public class ReflectionUtility
    {
        public static IEnumerable<FieldInfo> GetAllFields(Type t)
        {
            if (t == null)
                return Enumerable.Empty<FieldInfo>();

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.Static | BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;
            return t.GetFields(flags).Concat(GetAllFields(t.BaseType));
        }
    }

    [TestFixture]
    public class ReflectionUtilityTests
    {
        [Test]
        public void access_all_private_fields_from_inheritance_heirarchy()
        {
            var allFields = ReflectionUtility.GetAllFields(typeof (Employee));

            Assert.That(allFields.Any(item => item.Name.Equals("_hair")), Is.True);
            Assert.That(allFields.Any(item => item.Name.Equals("_address")), Is.True);
            Assert.That(allFields.Any(item => item.Name.Equals("_jobTitle")), Is.True);
        }

        public class Hair
        {
            public Color Color { get; set; }
        }

        public class Mammal
        {
            private Hair _hair;

            public Hair Hair
            {
                get { return _hair; }
                set { _hair = value; }
            }
        }

        public class Human : Mammal
        {
            private string _address;

            public string Address
            {
                get { return _address; }
                set { _address = value; }
            }
        }

        public class Employee : Human
        {
            private string _jobTitle;

            public string JobTitle
            {
                get { return _jobTitle; }
                set { _jobTitle = value; }
            }
        }    
    }


}