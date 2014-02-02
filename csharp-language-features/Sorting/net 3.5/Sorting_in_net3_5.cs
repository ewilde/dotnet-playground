using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Sorting
{
    /// <summary>
    /// Shows to important sorting methods that can be used with <see cref="List{T}"/> notably
    /// <see cref="List{T}.Sort()"/> and
    /// <see cref="Enumerable.OrderBy{TSource,TKey}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,TKey})"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="List{T}.Sort()"/> modifies the state of the recipient whereas order by returns a 
    /// new <see cref="IEnumerable{T}"/>.
    /// </remarks>
    class Sorting_in_net3_5
    {
        public void Run()
        {
            List<Product> products =
             new List<Product>(
                 Product.GetSampleProducts());
            products.Sort((x, y) => x.Name.CompareTo(y.Name));

            ConsoleUtility.PrintInfo("Sorting a list using a lambda expression, by name ascending.");
            ConsoleUtility.PrintSuccess(
                products.ToStringTable(
                    new[] { "Name", "Price" },
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));

            products =
             new List<Product>(
                 Product.GetSampleProducts());

            products = products.OrderBy(item => item.Name).ToList();
            ConsoleUtility.PrintInfo("Sorting a list without modifying the underlying list using orderby method and a lambda expression, by name ascending.");
            ConsoleUtility.PrintSuccess(
                products.ToStringTable(
                    new[] { "Name", "Price" },
                    item => item.Name,
                    item => string.Format("£ {0:0.00}", item.Price)));
        }
    }
}
