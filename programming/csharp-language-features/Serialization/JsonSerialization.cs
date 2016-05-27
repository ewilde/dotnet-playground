using System;
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
        
        [JsonProperty("change_date")]
        public DateTime LastChangedDateTime { get; set; }
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

        [Test]
        public void Deserialize()
        {
            string json = @"{
    ""first_name"" : ""Bob"",
    ""last_name"" : ""Hoskins"",
    ""change_date"": ""4/30/2016 10:24:17 AM"",
                          }";

            var employee = JsonConvert.DeserializeObject<Employee>(json);

            Assert.That(employee.FirstName, Is.EqualTo("Bob"));
        }
    }
}