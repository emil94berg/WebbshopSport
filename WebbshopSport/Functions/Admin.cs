using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebbshopSport.Models;

namespace WebbshopSport.Functions
{
    internal class Admin
    {
        public static void AddProduct()
        {
            using (var db = new MyDbContext())
            {
                var categories = (from Category in db.Categories
                                 select Category).ToList();
                var suppliers = (from Supplier in db.Suppliers
                                 select Supplier).ToList();
                Console.WriteLine("Name of product: ");
                string productName = Console.ReadLine();
                Console.WriteLine("Color/colors of product: ");
                string color = Console.ReadLine();
                Console.WriteLine("Short description about the product: ");
                string description = Console.ReadLine();
                Console.WriteLine("Price: ");
                int price = int.Parse(Console.ReadLine());
                Console.WriteLine("Choose supplier id: ");
                foreach (var supplier in suppliers)
                {
                    Console.Write(supplier.Id + " : " + supplier.Name + "\t");
                }
                int supplierId = int.Parse(Console.ReadLine());
                bool choosen = false;
                Console.WriteLine("Choose category id: ");
                foreach (var category in categories)
                {
                    Console.Write(category.Id + " : " + category.Name + "\t");
                }
                int categoryId = int.Parse(Console.ReadLine());
                Console.WriteLine("Starting ammount: ");
                int ammount = int.Parse(Console.ReadLine());

                Models.Product product1 = new Models.Product()
                {
                    Name = productName,
                    Color = color,
                    Description = description,
                    Price = price,
                    SupplierId = supplierId,
                    CategoryId = categoryId,
                    Ammount = ammount
                };
                db.Products.Add(product1);
                db.SaveChanges();


            }
        }
        public static void AddCategory()
        {
            using (var db = new MyDbContext())
            {
                var categoryList = db.Categories;
                Console.WriteLine("Name of category: ");
                string categoryName = Console.ReadLine();
                Models.Category category = new Models.Category()
                {
                    Name = categoryName
                };
                categoryList.Add(category);
                db.SaveChanges();
            }
        }
        public static void AddSupplier()
        {
            using (var db = new MyDbContext())
            {
                var supplierList = db.Suppliers;
                Console.WriteLine("Name of supplier: ");
                string supplierName = Console.ReadLine();
                Models.Supplier supplier = new Models.Supplier()
                {
                    Name = supplierName
                };
                supplierList.Add(supplier);
                db.SaveChanges();
            }
        }
        public static void AddSize()
        {
            using (var db = new MyDbContext())
            {
                Console.WriteLine("ProductId: ");
                int productId = int.Parse(Console.ReadLine());
                Console.WriteLine("SizeId: ");
                int sizeId = int.Parse(Console.ReadLine());
                Models.Size size = new Models.Size()
                {
                    Id = productId
                };
            }
        }
        public static void ChangeCategory()
        {
            using (var db = new MyDbContext())
            {
                var categoryList = db.Categories;
                var categories = (from Category in db.Categories
                                  select Category.Name).ToList();
                Console.WriteLine($"Which category do you want to update?\n 1: {categories[0]}\t 2: {categories[1]}\t {categories[2]}");
                int choosenCategory = int.Parse(Console.ReadLine());
                Console.WriteLine("New name: ");
                var updateCategory = db.Categories.First(x => x.Id == choosenCategory);
                updateCategory.Name = Console.ReadLine();
                db.SaveChanges();
            }
        }
        public static void ChangeSupplier()
        {
            using (var db = new MyDbContext())
            {
                var supplierList = db.Suppliers;
                var suppliers = (from Supplier in db.Suppliers
                                  select Supplier.Name).ToList();
                Console.WriteLine($"Which supplier do you want to update?\n1 - {suppliers[0]}\t 2: {suppliers[1]}\t {suppliers[2]}");
                int choosenSupplier = int.Parse(Console.ReadLine());
                Console.WriteLine("New name: ");
                var updateSupplier = db.Suppliers.First(x => x.Id == choosenSupplier);
                updateSupplier.Name = Console.ReadLine();
                db.SaveChanges();
            }
        }
        public static void DeleteSupplier()
        {
            using (var db = new MyDbContext())
            {
                int i = 1;
                var supplierList = db.Suppliers;
                
                Console.WriteLine($"Which supplier do you want to remove?");
                foreach (var supplier in supplierList)
                {
                    
                    Console.Write(i + ": " +supplier.Name + " ");
                    i++;
                }
                Console.WriteLine();
                int deleteId = int.Parse(Console.ReadLine());
                db.Remove(db.Suppliers.Single(x => x.Id == deleteId));
                db.SaveChanges();

            }
        }
        public static void DeleteProduct()
        {
            using (var db = new MyDbContext())
            {
                var productList = db.Products;

                Console.WriteLine($"Which product do you want to remove?");
                foreach (var products in productList)
                {

                    Console.Write(products.Id + ": " + products.Name + " ");
                    
                }
                Console.WriteLine();
                int deleteId = int.Parse(Console.ReadLine());
                db.Remove(db.Products.Single(x => x.Id == deleteId));
                db.SaveChanges();

            }
        }
    }
}
