using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Reporting.Minion
{
    public class StartController : ApiController
    {
        [Route("api/start")]
        public void Put([FromBody]string value)
        {
            Minion.OnStart();
        }
    }
}
