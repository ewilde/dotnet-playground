using System;
using System.Collections;
using System.Linq;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Sorting
{
    /// <summary>
    /// .Net 1.0
    /// Allows for an entity to have a default sort order. This is the .net 1.0
    /// </summary>
    class SortUsing_IComparer : Product, IComparable
    {
        public SortUsing_IComparer(string name, decimal price)
            : base(name, price)
        {
        }

        public int CompareTo(object obj)
        {
            Product first = (Product)this;
            Product second = (Product)obj;
            return first.Name.CompareTo(second.Name);     
        }
    }

    class SortUsing_IComparable_Example
    {
        public void Run()
        {
            ArrayList products =
                new ArrayList(
                    Product.GetSampleProducts().Select(item => new SortUsing_IComparer(item.Name, item.Price)).ToArray());
            products.Sort();

            ConsoleUtility.PrintInfo("IComparable sorting using a default method on an entity.");
            ConsoleUtility.PrintSuccess(
                products.ToArray(typeof (Product)).Cast<Product>().ToStringTable(
                    new[] {"Name", "Price"},
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));
        }
    }
}
