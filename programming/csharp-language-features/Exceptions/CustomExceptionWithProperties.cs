using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //
        public CustomException()
        {
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, string id) : base(message)
        {
            this.Id = id;
        }

        public CustomException(string message, Exception inner) : base(message, inner)
        {
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Id = info.GetString("Id");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Id", this.Id);
            
            // MUST call through to the base class to let it save its own state
            base.GetObjectData(info, context);
        }

        public string Id { get; set; }
    }

    [TestFixture]
    public class CustomExceptionTests
    {
        private const string Message = "The widget has unavoidably blooped out.";
        private const string Id = "Foo bar id";
        [Test]
        public void TestSerializableExceptionWithCustomProperties()
        {
            var ex =
                new CustomException(Message, Id);

            // Sanity check: Make sure custom properties are set before serialization
            Assert.AreEqual(Message, ex.Message, "Message");
            Assert.AreEqual(Id, ex.Id, "ex.ResourceName");
            
            // Save the full ToString() value, including the exception message and stack trace.
            string exceptionToString = ex.ToString();

            // Round-trip the exception: Serialize and de-serialize with a BinaryFormatter
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                // "Save" object state
                bf.Serialize(ms, ex);

                // Re-use the same stream for de-serialization
                ms.Seek(0, 0);

                // Replace the original exception with de-serialized one
                ex = (CustomException)bf.Deserialize(ms);
            }

            // Make sure custom properties are preserved after serialization
            Assert.AreEqual(Message, ex.Message, "Message");
            Assert.AreEqual(Id, ex.Id, "ex.ResourceName");
            
            // Double-check that the exception message and stack trace (owned by the base Exception) are preserved
            Assert.AreEqual(exceptionToString, ex.ToString(), "ex.ToString()");
        }
    }
}