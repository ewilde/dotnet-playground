using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private static async Task<bool> DownloadPageAsyncWithZip()
        {
            using (var client = new HttpClient(
               new HttpClientHandler
               {
                   AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
               }))

            {
                string page = "http://en.wikipedia.org/";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

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
            }

            return true;
        }

        [Test]
        public void TestDownloadPageAsync()
        {
            Assert.That(async () => await DownloadPageAsync(), Is.True);
        }

        [Test]
        public void TestDownloadPageAsyncWithZip()
        {
            Assert.That(async () => await DownloadPageAsyncWithZip(), Is.True);
        }
    }
}
