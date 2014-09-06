using System;
using System.Reflection;
using NUnit.Framework;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace Edward.Wilde.CSharp.Features.Enums
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumerationValue)
              where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();

        }     
    }

    public enum ProjectType
    {
        [Description("Unknown project type")]
        Unknown,
        
        Helper,
        
        [Description("Startup project")]
        Startup,
        
        [Description("Live")]
        Live
    }

    [TestFixture]
    public class EnumDescriptionTest
    {
        [Test]
        public void DescriptionIsUsedInsteadOfEnumName()
        {
            Assert.That(ProjectType.Startup.GetDescription(), Is.EqualTo("Startup project"));
        }
    }
}