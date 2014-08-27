using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edward.Wilde.CSharp.Features.Model
{
    class Supplier
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public static List<Supplier> GetSampleSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier { Id = 1, Name = "Audi"},
                new Supplier { Id = 2, Name = "BMW"},
                new Supplier { Id = 3, Name = "Ford"},
                new Supplier { Id = 4, Name = "Jaguar"},
            };
        }
    }
}
