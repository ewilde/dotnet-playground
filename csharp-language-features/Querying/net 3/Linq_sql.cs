using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Querying.net_3
{
    public class Linq_sql
    {
        public void Run()
        {
            ConsoleUtility.PrintInfo(".net 3.5 querying objects using linq to xml with a query expression.");

            string text = string.Empty;
            using (var db = new LinqDemoDataContext())
            {
                if (db.Products.Count() == 0)
                {
                    ConsoleUtility.PrintInfo("Populating products to database...");
                    var entities = Model.Product.GetSampleProducts().Select( item=>new Edward.Wilde.CSharp.Features.Querying.net_3.Product() { Id = item.Id, Name = item.Name, SupplierId = item.SupplierId, Price = item.Price});
                    db.Products.InsertAllOnSubmit(entities);
                }

                if (db.Suppliers.Count() == 0)
                {
                    ConsoleUtility.PrintInfo("Populating suppliers to database...");
                    var entities = Model.Supplier.GetSampleSuppliers().Select(item => new Edward.Wilde.CSharp.Features.Querying.net_3.Supplier() { Id = item.Id, Name = item.Name});
                    db.Suppliers.InsertAllOnSubmit(entities);
                }

                var items = from p in db.Products
                    join s in db.Suppliers on p.SupplierId equals s.Id
                    where p.Price > 10
                    orderby s.Name
                    select new {Name = p.Name, Price = p.Price, SupplierName = s.Name};

                text = items.ToStringTable(new[] { "Name", "Price", "Supplier" },
                  item => item.Name,
                  item => item.Price,
                  item => item.SupplierName);

                db.SubmitChanges();

                ConsoleUtility.PrintSuccess(text);
            }
            
           
            ConsoleUtility.BlankLine();
        }
    }
}
