using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Utilities;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Sorting
{
    /// <summary>
    /// .Net 2.0
    /// Allows for an entity to have a default sort order using a strongly typed comparable interface
    /// </summary>
    /// <remarks>
    /// <para>
    /// Note that this interface will not work with <see cref="ArrayList.Sort()"/> instead
    /// use one of the classes introduced by .net 2 such as <see cref="List{T}"/>.
    /// </para>
    /// <para>
    ///     <b>Benefits:</b>
    ///     <list type="bullet">
    ///         <item>
    ///             <term>No boxing</term>
    ///             <description>Generics avoid the need for boxing of value types.</description>
    ///         </item>
    ///     
    ///     </list>
    /// </para>
    /// </remarks>
    class ProductThatCanBeCompared : Product, IComparable<Product>
    {
        public ProductThatCanBeCompared(string name, decimal price)
            : base(name, price)
        {
        }

        public int CompareTo(Product other)
        {
            Product first = this;
            Product second = other;
            return first.Name.CompareTo(second.Name);
        }
    }

    class ProductThatCanBeCompared_net2_Example
    {
        public void Run()
        {
            List<ProductThatCanBeCompared> products =
                new List<ProductThatCanBeCompared>(
                    Product.GetSampleProducts().Select(item => new ProductThatCanBeCompared(item.Name, item.Price)));
            products.Sort();

            ConsoleUtility.PrintInfo("IComparable<T> sorting using a default method on an entity.");
            ConsoleUtility.PrintSuccess(
                products.ToStringTable(
                    new[] {"Name", "Price"},
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));
        }
   }
}
