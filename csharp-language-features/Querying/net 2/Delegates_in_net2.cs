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
    /// In .net 2 it is now possible to query objects using a predefined delegate <see cref="Predicate{T}"/>
    /// and a method <see cref="List{T}.FindAll"/>.
    /// </summary>
    class Delegates_in_net2
    {
        public void Run()
        {
            ConsoleUtility.PrintInfo(".Net 2 querying objects using a predicate delegate {T}.");
            List<Product> products = Product.GetSampleProducts();
            Predicate<Product> test = new Predicate<Product>(MatchProduct);
            List<Product> matches = products.FindAll(test);
            
            var text = matches.ToStringTable(new[] {"Name", "Price"},
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price));

            ConsoleUtility.PrintSuccess(text);
            ConsoleUtility.BlankLine();
        }

        private bool MatchProduct(Product p)
        {
            return p.Price > 10m;
        }
    }
}
