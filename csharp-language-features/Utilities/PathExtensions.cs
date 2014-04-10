using System;
using System.IO;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public static class PathExtensions
    {
        /// <summary>
        /// Returns the relative path of the target string compared with the source
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="kind"></param>
        /// <example>
        /// "c:\dev\foo".RelativePath("c:\dev\foo\proj.csproj")
        /// 
        /// returns "foo\proj.csproj"
        /// </example>
        /// <returns>Relative </returns>
        public static string RelativePath(this string source, string target, UriKind kind = UriKind.Absolute)
        {
            if (kind == UriKind.Relative)
            {
                source = Path.Combine(@"c:\", source);
                target = Path.Combine(@"c:\", target);
            }

            var file = new Uri(target);
            if (source[source.Length - 1] != Path.DirectorySeparatorChar)
                source += Path.DirectorySeparatorChar;
            var folder = new Uri(source); // Must end in a slash to indicate folder

            var relativePath =
                Uri.UnescapeDataString(
                    folder.MakeRelativeUri(file)
                        .ToString()
                        .Replace('/', Path.DirectorySeparatorChar)
                    );

            return relativePath;
        }
    }
}