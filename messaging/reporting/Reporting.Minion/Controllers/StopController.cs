using System.Collections.Generic;
using System.Web.Http;

namespace Reporting.Minion
{
    public class StopController : ApiController
    {
        [Route("api/stop")]
        public void Put([FromBody] string value)
        {
            Minion.OnStop();
        }
    }
}