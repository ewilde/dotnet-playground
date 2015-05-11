using System.IO;
using MsgPack;
using MsgPack.Serialization;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Serialization
{
    public static class MessagePackEncodingExtensions
    {
        public static byte[] SerializeUsingMessagePack<T>(this object value)
        {
            using (var mem = new MemoryStream())
            {
                var serializer = MessagePackSerializer.Get<T>();
                serializer.Pack(mem, value);

                return mem.GetBuffer();
            }
        }
        public static T DeSerializeUsingMessagePack<T>(this byte[] value)
        {
            using (var mem = new MemoryStream(value))
            {
             //   SerializationContext.Default.CompatibilityOptions.PackerCompatibilityOptions = PackerCompatibilityOptions.None;
                var serializer = MessagePackSerializer.Get<T>();
                return  serializer.Unpack(mem);                
            }
        }
    }


    [TestFixture]
    public class MessagePackEncodingExtensionsExample
    {
        [Test]
        public void can_serialize_list_of_objects()
        {
            var employee = new Message();
            employee.Values.Add(1);
            employee.Values.Add("string");
            employee.Values.Add(new byte[] { 10, 20, 30 });

            var serializedResult = employee.SerializeUsingMessagePack<Message>();

            Assert.That(serializedResult.Length, Is.GreaterThan(0));
        }

        [Test]
        public void can_deserialize_list_of_objects()
        {
            var employee = new Message();
            employee.Values.Add(1);
            employee.Values.Add("string");
            employee.Values.Add(new byte[] { 10, 20, 30 });

            var serializedResult = employee.SerializeUsingMessagePack<Message>();
            var deserialize = serializedResult.DeSerializeUsingMessagePack<Message>();

            Assert.That(deserialize.Values[0].GetType(), Is.TypeOf<int>());
            Assert.That(deserialize.Values[2], Is.EquivalentTo(new byte[] { 10, 20, 30 }));
        }
    }
}