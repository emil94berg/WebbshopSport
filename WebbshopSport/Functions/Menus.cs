using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbshopSport.GraphWindow;
using WebbshopSport.Models;

namespace WebbshopSport.Functions
{
    internal class Menus
    {
        public static void ShowCategory(int answear, int loggedIn)
        {
            using (var db = new MyDbContext())
            {
                var nameOfCategory = db.Categories.Where(x => x.Id == answear).Select(x => $"{x.Name}").SingleOrDefault();
                var showCategory = db.Products.Where(x => x.CategoryId == answear).Select(x => $"{x.Id} - {x.Name} - {x.Price}").ToList();
                var categoryWindow = new Window(nameOfCategory,0,0,showCategory);
                categoryWindow.Draw();
                //foreach (var item in showCategory)
                //{
                //    Console.WriteLine(item);
                    
                //}
                int ammount = showCategory.Count();
                Console.WriteLine("Choose product: ");
                int productAnswear = int.Parse(Console.ReadLine());
                var nameOfProduct = db.Products.Where(x => x.Id == productAnswear).Select(x => $"{x.Name}").SingleOrDefault(); // Används till window string
                var showProduct = db.Products.Where(x => x.Id == productAnswear).Select(x => $"Description : {x.Description}").ToList(); // Används till window list
                var itemToCart = db.Products.Where(x => x.Id == productAnswear).Select(x => x).SingleOrDefault(); // Plocka ut hela objektet för att lägga till i varukorg
                var choosenProductWindow = new Window(nameOfProduct, 0, ammount+2, showProduct);
                choosenProductWindow.Draw();
                Console.WriteLine("Press x to add this product into your cart or any key to get back to menu");
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                if (key.KeyChar == 'x')
                {
                    Console.WriteLine("How many do you wish to order: ");
                    int howMany = int.Parse(Console.ReadLine());
                    Console.WriteLine(itemToCart.Name + " " + itemToCart.Price + " sek. Quantity: " + howMany);
                    Console.WriteLine("Added to cart");

                    var addToCart = new OrderItem()
                    {
                        ShoppingCartId = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.ShoppingCartId).SingleOrDefault(),
                        ProductId = itemToCart.Id,
                        Price = (itemToCart.Price * howMany),
                        Quantity = howMany,
                        Payed = false
                    };
                    db.OrderItems.Add(addToCart);
                    db.SaveChanges();
                }
                else
                {
                    
                }
            }
        }
        public static void RemoveFromCart(int loggedIn)
        {
            using (var db = new MyDbContext())
            {
                int shoppingNumber = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.ShoppingCartId).SingleOrDefault();
                Console.WriteLine("Type productId: of the product you want to remove: ");
                int removeId = int.Parse(Console.ReadLine());
                //var myOrderItems = db.OrderItems.Where(x => x.ShoppingCartId == shoppingNumber).Select(x => x.ProductId == removeId).SingleOrDefault();
                db.Remove(db.OrderItems.Single(x => x.ProductId == removeId && x.ShoppingCartId == shoppingNumber));
                db.SaveChanges();

                //db.Remove(db.Suppliers.Single(x => x.Id == deleteId));
                //foreach (var item in myOrderItems)
                //{
                //    Console.WriteLine(item);
                //}
            }
        }
        public static void ShippmentView(int loggedIn)
        {
            using (var db = new MyDbContext())
            {
                string costumerName = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.Name).SingleOrDefault();
                string costumerAddress = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.Address).SingleOrDefault();
                string costumerCity = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.City).SingleOrDefault();
                string costumerZipCode = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.ZipCode).SingleOrDefault().ToString();
                string costumerCountry = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.Country).SingleOrDefault();
                string costumerPhone = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.PhoneNumber).SingleOrDefault();
                string costumerEmail = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.Email).SingleOrDefault();
                List<string> costumerList = new List<string>()
                {
                    costumerName,
                    costumerAddress,
                    costumerCity,
                    costumerCountry,
                    costumerZipCode,
                    costumerPhone,
                    costumerEmail
                };
                var costumerWindow = new Window("Check Details", 0, 0, costumerList);
                costumerWindow.Draw();

                

                var thisCostumer = db.Customers.Where(x => x.Id == loggedIn).Select(x => x.ShoppingCartId).SingleOrDefault();
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
                    var orderItemsList = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer && x.Payed == false).ToList();
                    for (int i = 0; i < orderItemsList.Count(); i++)
                    {
                        orderItemsView.Add($"ProductId: {orderItemsList[i].ProductId} {productNameFromOrderId[i]}. Quantity: {orderItemsList[i].Quantity}. Price: {orderItemsList[i].Price} :-");
                    }
                    //.Select(x => $"{productNameFromOrderId[0]}. Quantity: {x.Quantity}. Price: {x.Quantity * x.Price} :-").ToList();
                    var windowOrderItems = new Window("Items in cart", 0, (costumerList.Count + 2), orderItemsView);

                    if (orderItemsList.Count > 0)
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
                var allItemsInCart = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer && x.Payed == false).ToList();
                Console.WriteLine("Do you want to checkout y/n");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar == 'y' && allItemsInCart.Count > 0)
                {
                    var shipmentPrice = db.OrderItems.First(x => x.ShoppingCartId == thisCostumer && x.Payed == false);
                    Console.WriteLine("Press 1 for slow home delivery (10:-) or 2 for home fast delivery (25:-)");
                    ConsoleKeyInfo key2 = Console.ReadKey(true);
                    if(key.KeyChar == '1')
                    {
                        shipmentPrice.Price = (shipmentPrice.Price + 10);
                        db.SaveChanges();
                    }
                    else if(key.KeyChar == '2')
                    {
                        shipmentPrice.Price = (shipmentPrice.Price + 25);
                        db.SaveChanges();
                    }
                    else
                    {

                    }
                    
                    var findProductId = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer && x.Payed == false).Select(x => x.ProductId).ToList();
                    var findQuantity = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer && x.Payed == false).Select(x => x.Quantity).ToList();

                    for (int i = 0; i < findProductId.Count; i++)
                    {
                        var updateThis = db.Products.First(x => x.Id == findProductId[i]);
                        updateThis.Ammount = (updateThis.Ammount - findQuantity[i]);
                        
                        db.SaveChanges();

                    }
                    int totalPrice = 0;
                    var priceCount = db.OrderItems.Where(x => x.ShoppingCartId == thisCostumer && x.Payed == false).ToList();
                    foreach(var price in priceCount)
                    {
                        totalPrice += price.Price;
                    }
                    Console.WriteLine($"Total cost of this order: {totalPrice}");
                    Console.ReadKey();

                    foreach (var item in allItemsInCart)
                    {
                        item.Payed = true;
                    }



                    db.SaveChanges();
                }


            }
        }
        

    }
}
