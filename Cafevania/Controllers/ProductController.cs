using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cafevania.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace Cafevania.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ProductController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
            db.Open();
            List<Product> cats = db.Query<Product>("select * from Product").AsList<Product>();
            db.Close();

            return View(cats);

        }

        public IActionResult Details(int catid)
        {
            IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
            db.Open();
            Product cat = db.QuerySingle<Product>($"select * from Product where ID = {catid}");
            db.Close();

            return View(cat);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
