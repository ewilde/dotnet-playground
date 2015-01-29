using log4net;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    using System.Collections.Generic;
    using System;
    using System.IO;
    using System.Linq;

    namespace CityIndex.Build.Model.Utility
    {

        public static class FileUtility
        {


            /// <summary>
            /// Copy files combining source directory with each file name to the destination directory
            /// </summary>
            public static void CopyFiles(ILog log, IEnumerable<string> files, string sourceDirectory, string destinationDirectory, bool continueOnError = false)
            {
                foreach (var item in files)
                {
                    log.Debug("Copying file: " + Path.Combine(sourceDirectory, item) + " to: " + Path.Combine(destinationDirectory, item));
                    try
                    {
                        File.Copy(Path.Combine(sourceDirectory, item), Path.Combine(destinationDirectory, item), true);
                    }
                    catch (IOException ioException)
                    {
                        if (continueOnError)
                        {
                            log.Error("System.IO.IOException:" + ioException.Message);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            /// <summary>
            /// Copy all files from source directory except for the exclusions
            /// </summary>
            public static void CopyFiles(ILog log, string sourceDirectory, string destinationDirectory, bool continueOnError = false)
            {
                CopyFiles(log, sourceDirectory, destinationDirectory, new string[] { });
            }

            /// <summary>
            /// Copy all files from source directory except for the exclusions
            /// </summary>
            public static void CopyFiles(ILog log, string sourceDirectory, string destinationDirectory, IEnumerable<string> excludes, bool recursive = false)
            {
                var files = Directory.GetFiles(sourceDirectory, "*.*");
                foreach (var file in files.Where(
                  x =>
                  {
                      foreach (var exclude in excludes)
                      {
                          if (x.EndsWith(exclude))
                          {
                              return false;
                          }
                      }

                      return true;
                  }))
                {
                    try
                    {
                        var source = file;
                        var destination = Path.Combine(destinationDirectory, new FileInfo(file).Name);

                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                            log.Debug(destinationDirectory);
                        }

                        SetWritable(log, destination);
                        log.Debug("Writing file: " + source + " to " + destination);
                        File.Copy(source, destination, true);
                    }
                    catch (System.UnauthorizedAccessException unauth)
                    {
                        log.Error("System.UnauthorizedAccessException:" + unauth.Message);
                    }
                    catch (IOException ioException)
                    {
                        log.Error("System.IO.IOException:" + ioException.Message);
                    }
                }

                if (!recursive)
                {
                    return;
                }

                var dirs = Directory.GetDirectories(sourceDirectory);
                foreach (var dir in dirs)
                {
                    var dirInfo = new DirectoryInfo(dir);
                    CopyFiles(log, dir, Path.Combine(destinationDirectory, dirInfo.Name), excludes, true);
                }
            }

            /// <summary>
            /// Copy all files from source directory except for the exclusions
            /// </summary>
            public static void DeleteFiles(ILog log, string directory, IEnumerable<string> excludes)
            {
                var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
                foreach (var file in files.Where(
                  x =>
                  {
                      foreach (var exclude in excludes)
                      {
                          if (x.EndsWith(exclude))
                          {
                              return false;
                          }
                      }

                      return true;
                  }))
                {
                    try
                    {
                        log.Debug("Deleting file: " + file);
                        SetWritable(log, file);
                        File.Delete(file);
                    }
                    catch (System.UnauthorizedAccessException unauth)
                    {
                        log.Error("System.UnauthorizedAccessException:" + unauth.Message);
                    }
                    catch (IOException ioException)
                    {
                        log.Error("System.IO.IOException:" + ioException.Message);
                    }
                }
            }

            public static void SetWritable(ILog log, string path)
            {
                if (!File.Exists(path))
                {
                    return;
                }

                FileAttributes attributes = File.GetAttributes(path);

                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Make the file RW
                    attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly);
                    File.SetAttributes(path, attributes);
                    log.DebugFormat("The {0} file is no longer RO.", path);
                }
            }

            public static void FileTextReplace(ILog log, string file, string find, string replace)
            {
                SetWritable(log, file);
                string original = File.ReadAllText(file);
                File.WriteAllText(file, original.Replace(find, replace));
            }

            public static void FileTextReplace(ILog log, string file, string[] find, string[] replace)
            {
                SetWritable(log, file);
                string original = File.ReadAllText(file);

                for (int i = 0; i < find.Length; i++)
                {
                    original = original.Replace(find[i], replace[i]);
                }

                File.WriteAllText(file, original);
            }

            private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
            {
                return attributes & ~attributesToRemove;
            }

            public static readonly string[] WebExcludes = new[] { ".config", ".cs", ".csproj", ".user", ".vspscc" };
            public static readonly string[] WinExcludes = new[] { ".config", ".cs", ".csproj", ".user", ".vspscc" };

            public static readonly string[] ShellExcludes = new[] { ".config", ".cs", ".csproj", ".user", ".vspscc", ".cache", ".FileListAbsolute.txt", ".resx", ".bmp", "vshost.exe", "vshost.exe.manifest", ".log", ".csv" };
        }
    }
}