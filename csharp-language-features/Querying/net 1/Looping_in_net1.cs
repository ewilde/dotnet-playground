using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edward.Wilde.CSharp.Features.Model;
using Edward.Wilde.CSharp.Features.Sorting;
using Edward.Wilde.CSharp.Features.Utilities;

namespace Edward.Wilde.CSharp.Features.Querying
{
    public delegate ArrayList FindAll(ArrayList items);

    /// <summary>
    /// In .net 1 we really only had looping constructs for querying data structures.
    /// Here we demonstrate one of the more convienient loops as well as using delegates,
    /// which allow us to better seperate concerns.
    /// </summary>
    public class Looping_in_net1
    {

        public void Run()
        {
            ForEachMethod();

            ConsoleUtility.BlankLine();

            DelegateMethod();

            ConsoleUtility.BlankLine();       
        }

        private void DelegateMethod()
        {
            ConsoleUtility.PrintInfo(".Net 1 technique for finding items using a delegate find method, for better seperation of concerns.");
            
            var foundItems = new FindAll(FindProductsPriceGreater10).Invoke(new ArrayList(Product.GetSampleProducts()));
            foreach (Product product in foundItems)
            {
                ConsoleUtility.PrintSuccess(product.ToString());
            }
        }

        private void ForEachMethod()
        {
            ConsoleUtility.PrintInfo(".Net 1 technique for finding items using a for each loop.");
            
            foreach (var product in Product.GetSampleProducts())
            {
                if (product.Price > 10)
                {
                    ConsoleUtility.PrintSuccess(product.ToString());
                }
            }
        }

        private ArrayList FindProductsPriceGreater10(ArrayList items)
        {
            ArrayList foundItems = new ArrayList();
            foreach (Product item in items)
            {
                if (item.Price > 10)
                {
                    foundItems.Add(item);
                }
            }

            return foundItems;
        }
    }
}
