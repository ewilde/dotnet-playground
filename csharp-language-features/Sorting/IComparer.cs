using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Sorting
{
    class ProductDescendingNameComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Product first = (Product)x;
            Product second = (Product)y;
            return second.Name.CompareTo(first.Name);
        }
    }

    class ProductAscendingPriceComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Product first = (Product)x;
            Product second = (Product)y;
            return first.Price.CompareTo(second.Price);
        }
    }

    class SortUsing_IComparer_Example
    {
        public void Run()
        {
            ArrayList products =
                new ArrayList(Product.GetSampleProducts());
            products.Sort(new ProductDescendingNameComparer());

            ConsoleUtility.PrintInfo("IComparer sorting using a descending name comparer.");
            ConsoleUtility.PrintSuccess(
                products.ToArray(typeof(Product)).Cast<Product>().ToStringTable(
                    new[] { "Name", "Price" },
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));

            products.Sort(new ProductAscendingPriceComparer());

            ConsoleUtility.PrintInfo("IComparer sorting using a ascending price comparer.");
            ConsoleUtility.PrintSuccess(
                products.ToArray(typeof(Product)).Cast<Product>().ToStringTable(
                    new[] { "Name", "Price" },
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));
        }
    }
}
