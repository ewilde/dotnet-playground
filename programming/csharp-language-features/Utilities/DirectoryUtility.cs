using System.IO;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public static class DirectoryUtility
    {
        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}