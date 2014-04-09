using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Edward.Wilde.CSharp.Features.Serialization
{
    public static class XmlSerializationExtension
    {
        public static string XmlSerialize<T>(this T objectToSerialize, bool utf8 = true)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            var stringWriter = utf8 ? new Utf8StringWriter() : new StringWriter(); // otherwise utf-16
            var xmlWriter = new XmlTextWriter(stringWriter)
            {
                Formatting = Formatting.Indented
            };

            xmlSerializer.Serialize(xmlWriter, objectToSerialize);

            return stringWriter.ToString();
        }  
    }
}