using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Sorting;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Querying
{
    /// <summary>
    /// In .net 3.5 it is now possible to query objects using code that is even more readable and removes some of the fluff.
    /// </summary>
    class Linq_method_extensions_in_net3_5
    {
        public void Run()
        {
            ConsoleUtility.PrintInfo(".net 3.5 querying objects using  a linq method extension.");
            List<Product> products = Product.GetSampleProducts();

            var text = products.Where(p => p.Price > 10).ToStringTable(new[] { "Name", "Price" },
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price));
            ConsoleUtility.PrintSuccess(text);
            ConsoleUtility.BlankLine();
        }
    }
}
