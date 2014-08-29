using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbitmq.routing.producer
{
    class Program
    {
        static void Main(string[] args)
        {
            new EmitLogDirect().Run(args);
        }
    }
}
