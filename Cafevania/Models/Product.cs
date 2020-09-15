using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Data;

namespace Cafevania.Models
{
        [Table("Product")] //--------------------------------- Lets dapper know table name
        public class Product
        {
            [Key]
            public long ID { get; set; }


            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }


            //Static will perform action in one fell swoop
            public static Product Create(string _name, Decimal _price, string _category, string _description)
            {
                //Create new product instance
                Product prod = new Product() { Name = _name, Price = _price, Category = _category, Description = _description };

                //Save to db
                IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
                long _id = db.Insert<Product>(prod);

                //Set ID column
                prod.ID = _id;

                db.Close();
                //Return instance of product
                prod.Save();
                return prod;

            }

            public static Product Read(long _id)
            {
                IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
                Product prod = db.Get<Product>(_id);
                return prod;
            }

            public static List<Product> Read()
            {
                IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
                List<Product> cat = db.GetAll<Product>().ToList();
                return cat;
            }

            public static void Delete(long _id)
            {
                IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
                db.Delete(new Product() { ID = _id });
            }

            public void Save()
            {
                IDbConnection db = new SqlConnection("Server=52Y6Q13\\SQLEXPRESS;Database=Cafevania;user id=user4;password=pass1");
                db.Update(this);
            }

        }
}
