using System.Collections.Generic;
using System.Web.Http;

namespace Reporting.Minion
{
    public class MessageSizeController : ApiController
    {
        public void Put(int id, [FromBody]string value)
        {
            Minion.OnChangeMessageSize(new ChangeMessageSizeEventArgs(id));
        }     
    }
}