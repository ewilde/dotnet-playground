using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Serialization
{
    public static class BinarySerializationExtension
    {
        public static byte[] Serialize(this object value)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                bf.Serialize(ms, value);
                return ms.GetBuffer();
            }
        }

        public static T DeSerializeFromBase64<T>(this string value)
        {
            var formatter = new BinaryFormatter();
            using (var mem = new MemoryStream(Convert.FromBase64String(value)))
            {
                return (T)formatter.Deserialize(mem);
            }
        }

        public static string SerializeToBase64(this object value)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                bf.Serialize(ms, value);
                return Convert.ToBase64String(ms.GetBuffer());
            }
        }
    }

    [TestFixture]
    public class BinarySerializationExtensionExample
    {
        [Test]
        public void can_serialize_list_of_objects()
        {
            var employee = new Message();
            employee.Values.Add(1);
            employee.Values.Add("string");
            employee.Values.Add(new byte[] {10,20,30});

            var serializedResult = employee.SerializeToBase64();

            Assert.That(serializedResult.Length, Is.GreaterThan(0));
        }

        [Test]
        public void can_deserialize_list_of_objects()
        {
            var employee = new Message();
            employee.Values.Add(1);
            employee.Values.Add("string");
            employee.Values.Add(new byte[] {10,20,30});

            var serializedResult = employee.SerializeToBase64();
            var deserialize = serializedResult.DeSerializeFromBase64<Message>();
            
            Assert.That(deserialize.Values[0], Is.EqualTo(1));
            Assert.That(deserialize.Values[2], Is.EquivalentTo(new byte[] { 10, 20, 30 }));
        }
    }

    [Serializable]
    public class Message
    {
        public Message()
        {
            Values = new List<object>();
        }

        public List<object> Values { get; set; }
    }
}