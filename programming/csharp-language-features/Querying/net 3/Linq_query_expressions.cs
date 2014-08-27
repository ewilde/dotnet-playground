using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Sorting;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Querying.net_3
{
    class Linq_query_expressions
    {
        public void Run()
        {
            ConsoleUtility.PrintInfo(".net 3.5 querying objects using linq query expression.");
            var products = Model.Product.GetSampleProducts();
            var suppliers = Model.Supplier.GetSampleSuppliers();

            var items = from p in products 
                             join s in suppliers on p.SupplierId equals s.Id
                        where p.Price > 10
                        orderby s.Name, p.Name
                        select new {Name = p.Name, Supplier = s.Name, Price = p.Price};

            var text = items.ToStringTable(new[] { "Name", "Supplier", "Price" },
                    item => item.Name,
                    item => item.Supplier,
                    item => string.Format("£ {0:0.00}", item.Price));
            ConsoleUtility.PrintSuccess(text);
            ConsoleUtility.BlankLine();

        }
    }
}
