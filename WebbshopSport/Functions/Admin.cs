using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
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
                                  select Category).ToList();
                Console.WriteLine($"Which category do you want to update?");
                foreach (var category in categories)
                {
                    Console.Write($"{category.Id} {category.Name}\t");
                }
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
                                  select Supplier).ToList();
                Console.WriteLine($"Which supplier do you want to update?");
                foreach (var supplier in suppliers)
                {
                    Console.Write($"{supplier.Id} : {supplier.Name}\t");
                }
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
                var suppliers = (from Supplier in db.Suppliers
                                select Supplier).ToList();

                Console.WriteLine($"Which supplier do you want to remove?");
                foreach (var supplier in suppliers)
                {
                    
                    Console.Write($"{supplier.Id} : {supplier.Name}\t");
                    
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
        public static void ChangeProduct()
        {
            using (var db = new MyDbContext())
            {
                var products = db.Products;
                Console.WriteLine("Type the Id of the product you want to update: ");
                foreach (var product in products)
                {
                    Console.WriteLine(product.Id + " : " + product.Name);
                }
                int choosenProduct = int.Parse(Console.ReadLine());
                while (true)
                {
                    Console.Clear();
                    var updateProduct = db.Products.First(x => x.Id == choosenProduct);
                    string isChoosen = updateProduct.Choosen ? "Is choosen for frontpage" : "Is not choosen for frontpage";
                    Console.WriteLine($"1.Namn: {updateProduct.Name} | 2.Price: {updateProduct.Price} | 3.Ammount: {updateProduct.Ammount} | 4.CategoryId: " +
                        $"{updateProduct.CategoryId} | 5.Color: {updateProduct.Color} | 6.Description: {updateProduct.Description} | 7.SupplierId: {updateProduct.SupplierId} | 8. {isChoosen} \n" +
                        $"** Press 0 to exit **");
                    int numberUpdate = int.Parse(Console.ReadLine());
                    Console.Clear();
                    if (numberUpdate == 1)
                    {
                        Console.WriteLine("New name: ");
                        updateProduct.Name = Console.ReadLine();
                    }
                    else if (numberUpdate == 2)
                    {
                        Console.WriteLine("New price: ");
                        updateProduct.Price = int.Parse(Console.ReadLine());
                    }
                    else if (numberUpdate == 3)
                    {
                        Console.WriteLine("New ammount: ");
                        updateProduct.Ammount = int.Parse(Console.ReadLine());
                    }
                    else if (numberUpdate == 4)
                    {
                        var categoryName = db.Categories;
                        foreach (var category in categoryName)
                        {
                            Console.WriteLine($"{category.Id} : {category.Name}");
                        }
                        Console.WriteLine("New categoryId: ");
                        updateProduct.CategoryId = int.Parse(Console.ReadLine());
                    }
                    else if (numberUpdate == 5)
                    {
                        Console.WriteLine("New color");
                        updateProduct.Color = Console.ReadLine();
                    }
                    else if (numberUpdate == 6)
                    {
                        Console.WriteLine("New description");
                        updateProduct.Description = Console.ReadLine();
                    }
                    else if (numberUpdate == 7)
                    {
                        var supplierName = db.Suppliers;
                        foreach (var supplier in supplierName)
                        {
                            Console.WriteLine($"{supplier.Id} : {supplier.Name}");
                        }
                        Console.WriteLine("New supplierId");
                        updateProduct.SupplierId = int.Parse(Console.ReadLine());

                    }
                    else if (numberUpdate == 8)
                    {
                        Console.WriteLine("Do you want this product on the frontpage y/n?");
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.KeyChar == 'n')
                        {
                            updateProduct.Choosen = false;
                        }
                        else if (key.KeyChar == 'y')
                        {
                            updateProduct.Choosen = true;
                        }
                    }
                    else if (numberUpdate == 0)
                    {
                        return;
                    }
                    db.SaveChanges(); 
                }


            }
        }
        public static void CreateShoppingCostumer()
        {
            using (var db = new MyDbContext())
            {
                ShoppingCart cart = new ShoppingCart();
                db.ShoppingCarts.Add(cart);
                db.SaveChanges();

                Console.WriteLine("Name of costumer: ");
                string costumerName = Console.ReadLine();
                Console.WriteLine("Street: ");
                string street = Console.ReadLine();
                Console.WriteLine("City: ");
                string city = Console.ReadLine();
                Console.WriteLine("Country: ");
                string country = Console.ReadLine();
                Console.WriteLine("Email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Zip code: ");
                int zipCode = int.Parse(Console.ReadLine());
                Console.WriteLine("Phone number: ");
                string phoneNumber = Console.ReadLine();
                Console.WriteLine("Age: ");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("Admin? typ 'y' or any key to continue");
                string admin = Console.ReadLine();
                bool isAdmin = false;
                if (admin == "y")
                {
                    isAdmin = true;
                }
                else
                {
                    isAdmin = false;
                }
                var shoppingCartId = cart.Id;

                Models.Customer customer = new Models.Customer()
                {
                    Name = costumerName,
                    Address = street,
                    City = city,
                    Country = country,
                    Email = email,
                    ZipCode = zipCode,
                    PhoneNumber = phoneNumber,
                    Age = age,
                    Admin = isAdmin,
                    ShoppingCartId = shoppingCartId
                };
                
                db.Customers.Add(customer);
                db.SaveChanges();
            }











        }
    }
}
