using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbshopSport.Models;

namespace WebbshopSport.GraphWindow
{
    internal class Frontpage
    {
        public static void ChoosenProducts(int loggedIn)
        {
            using (var db = new MyDbContext())
            {
                List<String> Welcome = new List<String>()
                {
                    "Welcome to WebshopSport",
                    "We got sportstuff for you"

                };
                var welcomeWindow = new Window("",48, 0, Welcome);
                welcomeWindow.Draw();

                List<string> cat1 = new List<string>()
                {
                    ""
                };
                if (db.Products.Where(x => x.CategoryId == 1).Any(x => x.Choosen) == true)
                {
                    cat1 = db.Products.Where(x => x.CategoryId == 1 && x.Choosen == true).Select(x => $"{x.Name} | Price: {x.Price} | {x.Description}").ToList();
                }
                else
                {
                    cat1 = new List<string>()
                    {
                        "No offers here"
                    };
                }
                var windowCart = new Window("Offer in Football", 0, 4, cat1);
                windowCart.Draw();


                List<string> cat2 = new List<string>()
                {
                    ""
                };
                string testString = cat1.ToString();
                int fromLeft = (testString.Length);
                if (db.Products.Where(x => x.CategoryId == 2).Any(x => x.Choosen) == true)
                {
                    cat2 = db.Products.Where(x => x.CategoryId == 2 && x.Choosen == true).Select(x => $"{x.Name} | Price: {x.Price} | {x.Description}").ToList();
                }
                else
                {
                    cat2 = new List<string>()
                    {
                        "No offers here"
                    };
                }
                //var testList1 = db.Products.Where(x => x.CategoryId == 2 && x.Choosen == true).Select(x => $"{x.Name} | Price: {x.Price} | {x.Description}").ToList();
                var windowCart1 = new Window("Offer in Icehockey", fromLeft, 4, cat2);
                windowCart1.Draw();

                List<string> cat3 = new List<string>()
                {
                    ""
                };
                string testString1 = cat2.ToString();
                int fromLeft1 = (fromLeft + testString1.Length);
                if (db.Products.Where(x => x.CategoryId == 3).Any(x => x.Choosen) == true)
                {
                    cat3 = db.Products.Where(x => x.CategoryId == 3 && x.Choosen == true).Select(x => $"{x.Name} | Price: {x.Price} | {x.Description}").ToList();
                }
                else
                {
                    cat3 = new List<string>()
                    {
                        "No offers here"
                    };
                }

                //var testList2 = db.Products.Where(x => x.CategoryId == 3 && x.Choosen == true).Select(x => $"{x.Name} | Price: {x.Price} | {x.Description}").ToList();
                var windowCart2 = new Window("Offer in Basketball", fromLeft1, 4, cat3);
                windowCart2.Draw();

                var catergoryList = db.Categories.Select(x => $"{x.Id} : {x.Name}").ToList();
                var windowCategories = new Window("Categories", 0, 8, catergoryList);
                windowCategories.Draw();
                //Får ut shoppingCartId för den personen som är inloggad
                var thisCostumer = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.ShoppingCartId).SingleOrDefault();
                //Testar om shoppingcartId existerar hos den personen som är inloggad för att inte crasha
                var listCount = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer).Select(x => x.ShoppingCartId).ToList();
                if (listCount.Count > 0)
                {
                    var thisProductName = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer).Select(x => x.ProductId).ToList();

                    var productNameFromOrderId = (from orderItem in db.OrderItems
                                                  join products in db.Products
                                                  on orderItem.ProductId equals products.Id
                                                  where orderItem.ShoppingCartId == thisCostumer && orderItem.Payed == false
                                                  select products.Name).ToList();
                    List<string> orderItemsView = new List<string>();
                    //ändrade && false för att testa
                    var orderItemsList = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer && x.Payed == false).ToList();
                    for(int i = 0; i < orderItemsList.Count(); i++)
                    {
                        orderItemsView.Add($"ProductId: {orderItemsList[i].ProductId} {productNameFromOrderId[i]}. Quantity: {orderItemsList[i].Quantity}. Price: {orderItemsList[i].Price} :-");
                    }
                        //.Select(x => $"{productNameFromOrderId[0]}. Quantity: {x.Quantity}. Price: {x.Quantity * x.Price} :-").ToList();
                    var windowOrderItems = new Window("Items in cart", 48, 8, orderItemsView);

                    if (orderItemsView.Count > 0)
                    {
                        windowOrderItems.Draw();  
                    }
                    else
                    {

                    }
                }
                else
                {
                    // shoppingcartId ska vara costumer som är inloggad shoppingcartId ist för 1
                    //ska visa en list med allt i orderitems som är kopplat till den inloggade personen
                }

                List<string> extra = new List<string>()
                {
                    "Press A for admin",
                    "Press S to search",
                    "Press Y to alter shoppingcart",
                    "Press C to checkout"
                };
                var extraWindow = new Window("", 109, 8, extra);
                extraWindow.Draw();

                
            }

        }
        public static void LogIn()
        {
            using (var db = new MyDbContext())
            {
                var loggedIn = db.Customers.Select(x => $"{x.Id} : {x.Name}").ToList();
                var windowAllCostumers = new Window("Costumers", 0, 0, loggedIn);
                windowAllCostumers.Draw();
            }
        }
        public static void LoggedIn(int i)
        {
            using (var db = new MyDbContext())
            {
                var thisPerson = db.Customers.Where(x => x.Id == i).Select(x => $"{x.Name}").ToList();
                var windowLoggedIn = new Window("LoggedIn", 0, 0, thisPerson);
                windowLoggedIn.Draw();
            }
        }
        public static void AdminView(int isAdmin)
        {
            using (var db = new MyDbContext())
            {
                Console.Clear();
                bool isPersonAdmin = db.Customers.Where(x => x.Id == isAdmin).Select(x => x.Admin).SingleOrDefault();
                List<String> list = new List<String>()
                {
                    "Press t to add costumer",
                    "Press p to add Product",
                    "Press c to add Category",
                    "Press s to add Supplier",
                    "Press x to update Product",
                    "Press o to update Category",
                    "Press e to update Supplier",
                    "Press r to delete Product",
                    "Press u to delete Supplier",
                    "Press 1 to see Query",
                    "Press 2 to delete Category",
                    "Press q to quit"
                };
                while (true)
                {

                    if (isPersonAdmin == true)
                    {
                        Console.Clear();
                        var Adminwindow = new Window("Admin view", 0, 0, list);
                        Adminwindow.Draw();
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.KeyChar)
                        {
                            case 't':
                                Functions.Admin.CreateShoppingCostumer();
                                break;
                            case 'p':
                                Functions.Admin.AddProduct();
                                break;
                            case 'c':
                                Functions.Admin.AddCategory();
                                break;
                            case 's':
                                Functions.Admin.AddSupplier();
                                break;
                            case 'x':
                                Functions.Admin.ChangeProduct();
                                break;
                            case 'o':
                                Functions.Admin.ChangeCategory();
                                break;
                            case 'e':
                                Functions.Admin.ChangeSupplier();
                                break;
                            case 'r':
                                Functions.Admin.DeleteProduct();
                                break;
                            case 'u':
                                Functions.Admin.DeleteSupplier();
                                break;
                            case 'q':
                                return;
                            case '1':
                                Functions.Query.Shoppoholic();
                                break;
                            case '2':
                                Functions.Admin.DeleteCategory();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You are not an Admin..");
                        return;
                    } 
                }
                
            }
        }
    }
}
