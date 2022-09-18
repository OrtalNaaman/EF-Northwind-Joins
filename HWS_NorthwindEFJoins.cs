using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthwindConsoleApp.Models;


using System;

namespace HWS_NorthwindEFJoins
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ex1
            var db = new NorthwindContext();
            var productsList = db.Products.ToList();

            var productInStock = from product in productsList
                                 select new { product.ProductName, product.UnitsInStock };        

            //Ex2
            var currentProductsList = from product in productsList
                                      where product.Discontinued == false
                                      select new { product.ProductId, product.ProductName };

            //Ex3
            var discontinuedProductsList = from product in productsList
                                           where product.Discontinued == true
                                           select new { product.ProductId, product.ProductName };

            //Ex4
            var maxAndMinPrices = from product in productsList
                                  orderby product.UnitPrice descending
                                  select new { product.ProductName, product.UnitPrice };

            //Ex5
            var productsLessThan20 = from product in productsList
                                     where product.UnitPrice < 20 && product.Discontinued == false
                                     orderby product.UnitPrice descending
                                     select new { product.ProductId, product.ProductName, product.UnitPrice };

            //Ex6
            var productsBetween15_25 = from product in productsList
                                       where product.UnitPrice >= 15 && product.UnitPrice <= 25
                                       orderby product.UnitPrice
                                       select new { product.ProductId, product.ProductName, product.UnitPrice };

            //Ex7
            var productsOboveAvgPrice = from product in productsList
                                        where product.UnitPrice > (productsList.Average(p => p.UnitPrice))
                                        orderby product.UnitPrice
                                        select new { product.ProductName, product.UnitPrice };

            //Ex8
            var TenMostExpensiveProducts = (from product in productsList
                                            orderby product.UnitPrice descending
                                            select new { product.ProductName, product.UnitPrice }).Take(10);

            //Ex9
            var countProducts = (from product in productsList
                                 group product by product.Discontinued into pd
                                 select new { pd = pd.Key, count = pd.Count()});

            //Ex10
            var productsManagement = from product in productsList
                                     where product.UnitsInStock < product.UnitsOnOrder && product.Discontinued == false
                                     select new { product.ProductName, product.UnitsOnOrder, product.UnitsInStock };
        }
    }
}



