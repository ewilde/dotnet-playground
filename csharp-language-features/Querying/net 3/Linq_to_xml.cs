using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Querying.net_3
{
    class Linq_to_xml
    {
        public void Run()
        {
            ConsoleUtility.PrintInfo(".net 3.5 querying objects using linq to xml with a query expression.");

            var doc = XDocument.Parse(Xml);
            var items = from p in doc.Descendants("Product")
                           join s in doc.Descendants("Supplier")
                           on (int)p.Attribute("SupplierID") equals (int)s.Attribute("SupplierID")
                           where (decimal)p.Attribute("Price") > 10
                           orderby (string)s.Attribute("Name"), (string)p.Attribute("Name")
                           select new
                           {
                               SupplierName = (string)s.Attribute("Name"),
                               ProductName = (string)p.Attribute("Name")
                           };

            var text = items.ToStringTable(new[] { "Name", "Supplier" },
                    item => item.ProductName,
                    item => item.SupplierName);
            ConsoleUtility.PrintSuccess(text);
            ConsoleUtility.BlankLine();

        }
        private const string Xml = 
            @"<?xml version=""1.0""?>
                <Data>
                <Products>
                    <Product Name=""West Side Story"" Price=""9.99"" SupplierID=""1"" />
                    <Product Name=""Assassins"" Price=""14.99"" SupplierID=""2"" />
                    <Product Name=""Frogs"" Price=""13.99"" SupplierID=""1"" />
                    <Product Name=""Sweeney Todd"" Price=""10.99"" SupplierID=""3"" />
                </Products>
                <Suppliers>
                    <Supplier Name=""Solely Sondheim"" SupplierID=""1"" />
                    <Supplier Name=""CD-by-CD-by-Sondheim"" SupplierID=""2"" />
                    <Supplier Name=""Barbershop CDs"" SupplierID=""3"" />
                </Suppliers>
                </Data>";
    }
}
