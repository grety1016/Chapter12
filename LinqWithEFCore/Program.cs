﻿using System;
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
                .ProcessSequence()
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

        static void GroupJoinCategoriesAndProducts()
        {
            using (var db = new Northwind())
            {
                var queryGroup = db.Categories.AsEnumerable().GroupJoin(
                    inner:db.Products,
                    outerKeySelector:Category => Category.CategoryID,
                    innerKeySelector:Product => Product.CategoryID,
                    resultSelector:(c,matchingProducts) =>new{
                        c.CategoryName,
                        proudcts = matchingProducts.OrderBy(p => p.ProductName)
                    });

                    foreach(var item in queryGroup)
                    {
                        WriteLine();
                        WriteLine("{0} has {1} products.",
                        arg0:item.CategoryName,
                        arg1:item.proudcts.Count());

                        foreach(var product in item.proudcts)
                        {
                            WriteLine($"{product.ProductName}");                            
                        }
                    }
            }
        }

        static void AggregateProducts()
        {
            using (var db = new Northwind() )
            {
                WriteLine("{0,-25} {1,10}",
                arg0: "Product count:",
                arg1: db.Products.Count()) ;

                WriteLine("{0,-25} {1,10:$#,##0.00}",
                arg0: "Highest product price:",
                arg1: db.Products.Max(p => p.UnitPrice)) ;

                WriteLine("{0,-25} {1,10:N0}",
                arg0: "Sum of units in stock: ",
                arg1: db.Products.Sum(p => p.UnitsInStock)) ;

                WriteLine("{0,-25} {1,10:N0}",
                arg0: "Sum of units on order: ",
                arg1: db.Products.Sum(p => p.UnitsOnOrder)) ;
                WriteLine("{0,-25} {1,10:$#,##0.00}",
                arg0: "Average unit price: ",
                arg1: db.Products.Average(p => p.UnitPrice) ) ;
                WriteLine("{0,-25} {1,10:$#,##0.00}",
                arg0: "Value of units in stock: ",
                arg1: db.Products.AsEnumerable()
                .Sum(p => p.UnitPrice * p.UnitsInStock)) ;
            }
        }

        static void CustomExtensionMethods()
        {
            using (var db = new Northwind() )
            {
                WriteLine("Mean units in stock:{0:N0}",
                db. Products. Average(p => p. UnitsInStock) ) ;
                WriteLine("Mean unit price: {0:$#,##0.00}",
                db. Products. Average(p => p. UnitPrice) ) ;
                WriteLine("Median units in stock: {0:N0}",
                db. Products. Median(p => p. UnitsInStock) ) ;
                WriteLine("Median unit price: {0:$#,##0.00}",
                db. Products. Median(p => p. UnitPrice) ) ;
                WriteLine("Mode units in stock: {0:N0}",
                db. Products. Mode(p => p. UnitsInStock) ) ;
                WriteLine("Mode unit price: {0:$#,##0.00}",
                db. Products. Mode(p => p. UnitPrice) ) ;
            }
        }
        static void Main(string[] args)
        {
            // var names = new string[] { "Michael", "Pam", "Jim", "Dwight","Angela", "Kevin", "Toby", "Creed" };
            // var query = (from name in names where name.Length > 4
            //                 select name)
            // .Skip(2)
            // .Take(2) ;
            // var query = from name in names
            // where name.Length > 4
            // orderby name.Length, name
            // select name;

            // foreach(var item in query)
            // {
            //     WriteLine($"{item}");
            // }
            CustomExtensionMethods();
            //FileAndSort();
            //JoinCategoriesAndProducts();
            //GroupJoinCategoriesAndProducts();
            //AggregateProducts();
        }
    }
}
