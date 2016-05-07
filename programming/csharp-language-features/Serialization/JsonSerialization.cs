using System.Diagnostics;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Serialization
{
    public class Employee
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
    [TestFixture]
    public class JsonSerializationExamples
    {        
        [Test]
        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(new Employee {FirstName = "Bob", LastName = "Hoskins"}, Formatting.Indented);
            Debug.WriteLine(json);
        }
    }
}