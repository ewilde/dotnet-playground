using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features
{
    public class ConsoleRedirect
    {
        public ProcessResult Run()
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
                    FileName = @"C:\Chocolatey\lib\NuGet.CommandLine.2.5.0\tools\Nuget.exe",
                    Arguments = "foo"
                }
            };

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

        [Test]
        public void Test()
        {
            var redirect = new ConsoleRedirect();
            var result = redirect.Run();

            Debug.WriteLine(string.Format("Standard output: " + result.ErrorOutput));
            Debug.WriteLine(string.Format("ErrorOutput output: " + result.StandardOutput));
            Debug.WriteLine(string.Format("Exit code: " + result.ExitCode));            
        }
    }

    public class ProcessResult
    {
        public string StandardOutput { get; set; }
        public string ErrorOutput { get; set; }
        public int ExitCode { get; set; }
    }
}