using System.Diagnostics;
using System.IO;
using System.Text;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    /// <summary>
    /// Runs a process, capturing the standard output, error output and exit code.
    /// </summary>
    public class ProcessExtensions
    {
        public static ProcessResult Run(string fileName, string arguments, string workingDirectory = null)
        {
            var error = new StringBuilder();
            var output = new StringBuilder();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = fileName,
                    Arguments = arguments,
                }
            };

            if (workingDirectory != null)
            {
                process.StartInfo.WorkingDirectory = workingDirectory;
            }

            process.ErrorDataReceived += (sender, args) => error.Append(args.Data);
            process.OutputDataReceived += (sender, args) => output.Append(args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            return new ProcessResult
            {
                StandardOutput = output.ToString(),
                ErrorOutput = error.ToString(),
                ExitCode = process.ExitCode
            };
        }

    }

    public class ProcessResult
    {
        public string StandardOutput { get; set; }
        public string ErrorOutput { get; set; }
        public int ExitCode { get; set; }
    }

    public class ProcessExtensionsExample
    {
        public void Run()
        {
            var result = ProcessExtensions.Run(
                   Path.Combine(@"C:\utils", "Nuget.exe"),
                   string.Format("restore {0} -Verbosity detailed -ConfigFile {1}", "packages.config", "NuGet.config"),
                   workingDirectory: @"C:\utils");

            Debug.WriteLine(result.ExitCode > 0
                ? string.Format("Error processing {0}. {1}", "packages.config", result.ErrorOutput)
                : string.Format("{0}", result.StandardOutput));
        }
    }
}