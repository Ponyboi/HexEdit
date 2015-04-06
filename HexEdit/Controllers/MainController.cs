using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TestingProject.Models;

namespace TestingProject.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        private class Product
        {
            public string OrderID { get; set; }
            public string OrderTotal { get; set; }
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string ProductQty { get; set; }
            public string ProductPrice { get; set; }
            public string ActivityID { get; set; }
        }

        public ActionResult Index()
        {
            StringBuilderTest stringBuilderTest = new StringBuilderTest();
            StringBuilder gaItemsScript = new StringBuilder();
            List<Product> Products = new List<Product>();

            Product product = new Product();
            product.OrderID = "1";
            product.OrderTotal = "100";
            product.ProductID = "1337";
            product.ProductName = "Item";
            product.ProductQty = "3";
            product.ProductPrice = "100";
            Products.Add(product);

            gaItemsScript.AppendFormat(
                "ga(\"ecommerce:addTransaction\", {{\"id\" : \"{0}\", \"affiliation\": \"{1}\", \"revenue\": \"{2}\", \"shipping\" : \"{3}\", \"tax\" : \"{4}\"}});\n",
                1,
                "Straz Center Shop",
                100,
                "",
                ""
            );


            foreach (Product cartItem in Products)
            {
                gaItemsScript.AppendFormat("ga(\"ecommerce:addItem\", {{\"id\": \"{0}\", \"name\": \"{1}\", \"sku\": \"{2}\", \"category\": \"{3}\", \"price\": \"{4}\", \"quantity\": \"{5}\"}});\n",
                    cartItem.OrderID,
                    cartItem.ProductName,
                    cartItem.ProductID,
                    "",
                    cartItem.ProductPrice,
                    cartItem.ProductQty
                );
            }
            gaItemsScript.Append("ga(\"ecommerce:send\");\n");

            stringBuilderTest.scriptString = gaItemsScript.ToString();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "gaItemsScript", gaItemsScript.ToString(), true);


            return View(stringBuilderTest);
        }

    }
}
