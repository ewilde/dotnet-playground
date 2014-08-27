using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Sorting
{
    /// <summary>
    /// Shows how to create comparer classes in the .net 2 style, that are strongly typed, removing the need for casting and
    /// removing the possibility of runtime errors due to invalid objects being cast.
    /// </summary>
    /// <remarks>
    /// Note that this interface will not work with <see cref="ArrayList.Sort()"/> instead
    /// use one of the classes introduced by .net 2 such as <see cref="List{T}"/>.
    /// </remarks>
    class ProductDescendingNameComparer_net2 : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            Product first = x;
            Product second = y;
            return second.Name.CompareTo(first.Name);
        }
    }

    class ProductAscendingPriceComparer_net2 : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            Product first = x;
            Product second = y;
            return first.Price.CompareTo(second.Price);
        }
    }

    internal class SortUsing_IComparer_net2_Example
    {
        public void Run()
        {
            List<Product> products =
                new List<Product>(Product.GetSampleProducts());
            products.Sort(new ProductDescendingNameComparer_net2());

            ConsoleUtility.PrintInfo("IComparer<T> sorting using a descending name comparer.");
            ConsoleUtility.PrintSuccess(
                products.ToStringTable(
                    new[] { "Name", "Price" },
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));

            products.Sort(new ProductAscendingPriceComparer_net2());

            ConsoleUtility.PrintInfo("IComparer<T> sorting using a ascending price comparer.");
            ConsoleUtility.PrintSuccess(
                products.ToStringTable(
                    new[] { "Name", "Price" },
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));
        }
    }

    class SortUsing_IComparer_net2_delegate_Example
    {
        public void Run()
        {
            List<Product> products =
                new List<Product>(
                    Product.GetSampleProducts().Select(item => new ProductThatCanBeCompared(item.Name, item.Price)).ToArray());
            products.Sort(delegate(Product x, Product y) { return x.Name.CompareTo(y.Name); });

            ConsoleUtility.PrintInfo("IComparable<T> sorting using a delegate, by name ascending.");
            ConsoleUtility.PrintSuccess(
                products.ToStringTable(
                    new[] { "Name", "Price" },
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));
        }
    }
}
