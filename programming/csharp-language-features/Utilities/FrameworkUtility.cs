using System;

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
}