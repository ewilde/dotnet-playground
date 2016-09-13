using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edward.Wilde.CSharp.Features.System.IO
{
    public interface IFileSystem
    {
        bool DirectoryExists(string path);
        bool FileExists(string path);
    }

    public class FileSystem : IFileSystem
    {
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}
