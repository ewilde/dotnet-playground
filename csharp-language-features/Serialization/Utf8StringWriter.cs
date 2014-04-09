using System.IO;
using System.Text;

namespace Edward.Wilde.CSharp.Features.Serialization
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}