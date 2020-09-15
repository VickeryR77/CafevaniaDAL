using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Dapper.Contrib.Extensions;
using System.Data;
using Cafevania.Models;
using System.Data.SqlClient;
using Dapper;

namespace Cafevania.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {

            IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
            db.Open();
            List<Product> cats = db.Query<Product>("select * from Product").AsList<Product>();
            db.Close();

            return View(cats);

            //1 Creates item
            //Product prod = Product.Create("Kitty Litter", 13.95m);
            //return Content($"{prod.ID} {prod.Name} {prod.Price}");

            //2 Reads
            //Product prod = Product.Read(2);
            //return Content($"{prod.ID} {prod.Name} {prod.Price}");

            //3 Views count
            //Product count, forloop --V < -- RICK NOTE INSERTED TO TRY TO VIEW A FULL LIST THROUGH DAPPER(ORM)
            //List<Product> prods = Product.Read();
            //return Content(prods.Count.ToString());

            //4 Delete
            //Product.Delete(5);
            //return Content("OK");

            //5 Update
            //Product prod = Product.Read(2);
            //prod.Price = 19.00m;
            //prod.Save();
            //return Content($"{prod.ID} {prod.Name} {prod.Price}");

            //return View();
        }

        [HttpPost]
        public IActionResult Index(string Name, Decimal Price, string Category, string Description)
        {
            IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
            Product.Create(Name, Price, Category, Description);
            List<Product> cats = db.Query<Product>("select * from Product").AsList<Product>();
            return View("Index", cats);
        }


        public IActionResult Update(long catid, string Name, Decimal Price, string Category, string Description)
        {
            if (HttpContext.Request.Method == "GET")
            {
                ViewBag.ID = catid;
                return View();
            }
            else
            {
                Product prod = Product.Read(catid);
                prod.Name = Name;
                prod.Price = Price;
                prod.Category = Category;
                prod.Description = Description;
                prod.Save();

                return RedirectToAction("Index", "Admin");
            }
        }

        public IActionResult Delete(long catid)
        {
            if (HttpContext.Request.Method == "GET")
            {
                ViewBag.ID = catid;
                return View();
            }

            Product.Delete(catid);
            return RedirectToAction("Index","Admin");
        }

        public IActionResult Edit()
        {
            return View();
        }

        
    }
}
