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

                var testList = db.Products.Where(x => x.CategoryId == 1 && x.Choosen == true).Select(x => $"{x.Name} | Price: {x.Price} | {x.Description}").ToList();
                //var textBox1 = db.Categories.Where(x => x.Id == 1).Select(x => $"{x.Name}").SingleOrDefault().ToString();
                var windowCart = new Window("Offer in Football", 0, 4, testList);
                windowCart.Draw();


                string testString = testList.ToString();
                int fromLeft = (testString.Length);
                var testList1 = db.Products.Where(x => x.CategoryId == 2 && x.Choosen == true).Select(x => $"{x.Name} | Price: {x.Price} | {x.Description}").ToList();
                var windowCart1 = new Window("Offer in Icehockey", fromLeft, 4, testList1);
                windowCart1.Draw();

                string testString1 = testList1.ToString();
                int fromLeft1 = (fromLeft + testString1.Length);
                var testList2 = db.Products.Where(x => x.CategoryId == 3 && x.Choosen == true).Select(x => $"{x.Name} | Price: {x.Price} | {x.Description}").ToList();
                var windowCart2 = new Window("Offer in Basketball", fromLeft1, 4, testList2);
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
                                                  where orderItem.ShoppingCartId == thisCostumer
                                                  select products.Name).ToList();
                    List<string> orderItemsView = new List<string>();
                    var orderItemsList = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer).ToList();
                    for(int i = 0; i < orderItemsList.Count(); i++)
                    {
                        orderItemsView.Add($"{productNameFromOrderId[i]}. Quantity: {orderItemsList[i].Quantity}. Price: {(orderItemsList[i].Quantity * orderItemsList[i].Price)} :-");
                    }
                        //.Select(x => $"{productNameFromOrderId[0]}. Quantity: {x.Quantity}. Price: {x.Quantity * x.Price} :-").ToList();
                    var windowOrderItems = new Window("Items in cart", 48, 8, orderItemsView);
                    windowOrderItems.Draw(); 
                }
                else
                {
                    // shoppingcartId ska vara costumer som är inloggad shoppingcartId ist för 1
                    //ska visa en list med allt i orderitems som är kopplat till den inloggade personen
                }



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
