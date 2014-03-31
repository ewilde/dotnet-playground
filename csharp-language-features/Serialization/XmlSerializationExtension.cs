using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Edward.Wilde.CSharp.Features.Serialization
{
    public static class XmlSerializationExtension
    {
        public static string XmlSerialize<T>(this T objectToSerialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter)
            {
                Formatting = Formatting.Indented
            };

            xmlSerializer.Serialize(xmlWriter, objectToSerialize);

            return stringWriter.ToString();
        }  
    }
}