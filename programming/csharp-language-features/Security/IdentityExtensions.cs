using System;
using System.Security.Principal;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Security
{
    public static class IdentityExtensions
    {
        public static bool IsElevated(this WindowsIdentity value)
        {
            var principal = new WindowsPrincipal(value);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    [TestFixture]
    public class IdentityExtensionsTests
    {
        [Test]
        public void Is_elevated()
        {
            Console.WriteLine(WindowsIdentity.GetCurrent().IsElevated());
        }
    }
}