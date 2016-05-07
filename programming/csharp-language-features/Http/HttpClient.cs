using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Http
{
    public class HttpClientExample
    {
        static async Task<bool> DownloadPageAsync()
        {
            // ... Target page.
            string page = "http://en.wikipedia.org/";

            // ... Use HttpClient.
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(page))
            using (var content = response.Content)
            {
                // ... Read the string.
                string result = await content.ReadAsStringAsync();

                // ... Display the result.
                if (result != null &&
                result.Length >= 50)
                {
                    Console.WriteLine(result.Substring(0, 500) + "...");
                }
            }

            return true;
        }

        [Test]
        public void TestDownloadPageAsync()
        {
            Assert.That(async () => await DownloadPageAsync(), Is.True);
        }
    }
}
