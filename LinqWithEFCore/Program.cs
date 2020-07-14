using System;
using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LinqWithEFCore
{
    class Program
    {

        static void FileAndSort()
        {
            using(var db = new Northwind())
            {
                var query = db.Products
                .Where(product => product.UnitPrice < 10M)
                .OrderByDescending(Product => Product.UnitPrice)
                .Select(product => new
                {
                    product.ProductID,
                    product.ProductName,
                    product.UnitPrice
                });

                WriteLine("That product cost less than $10: ");

                foreach(var item in query)
                {
                   WriteLine(format:"{0}:{1} cost {2:$#,##0.00}",
                   arg0:item.ProductID,
                   arg1:item.ProductName,
                   arg2:item.UnitPrice); 
                }
                WriteLine();

            }

        }

        static void JoinCategoriesAndProducts()
        {
            using(var db = new Northwind())
            {
                //join every product to its category to return 77 matches
                var joinquery = db.Categories.Join(
                inner:db.Products,
                outerKeySelector:Category => Category.CategoryID,
                innerKeySelector:Product => Product.CategoryID,
                resultSelector:(c,p) =>new {c.CategoryName,p.ProductName,p.ProductID})
                .OrderBy(cp => cp.ProductID);

                foreach(var item in joinquery)
                {
                    WriteLine("{0}:{1} is in {2}.",
                    arg0:item.ProductID,
                    arg1:item.ProductName,
                    arg2:item.CategoryName);
                }
            }

        }
        static void Main(string[] args)
        {
            //FileAndSort();
            JoinCategoriesAndProducts();

        }
    }
}
