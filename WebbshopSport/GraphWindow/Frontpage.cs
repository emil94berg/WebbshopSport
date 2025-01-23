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
        public static void ChoosenProducts()
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
    }
}
