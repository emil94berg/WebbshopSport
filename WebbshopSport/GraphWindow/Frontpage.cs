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
                    "We got 15 products"

                };
                var welcomeWindow = new Window("",48, 0, Welcome);
                welcomeWindow.Draw();

                var testList = db.Products.Where(x => x.CategoryId == 1 && x.Choosen == true).Select(x => $"{x.Name} * Price: {x.Price} * {x.Description}").ToList();
                //var textBox1 = db.Categories.Where(x => x.Id == 1).Select(x => $"{x.Name}").SingleOrDefault().ToString();
                var windowCart = new Window("Choosen product 1", 0, 4, testList);
                windowCart.Draw();


                string testString = testList.ToString();
                int fromLeft = (testString.Length);
                var testList1 = db.Products.Where(x => x.CategoryId == 1 && x.Choosen == true).Select(x => $"{x.Name} * Price: {x.Price} * {x.Description}").ToList();
                var windowCart1 = new Window("Choosen product 2", fromLeft, 4, testList1);
                windowCart1.Draw();

                string testString1 = testList1.ToString();
                int fromLeft1 = (fromLeft + testString1.Length);
                var testList2 = db.Products.Where(x => x.CategoryId == 1 && x.Choosen == true).Select(x => $"{x.Name} * Price: {x.Price} * {x.Description}").ToList();
                var windowCart2 = new Window("Choosen product 3", fromLeft1, 4, testList2);
                windowCart2.Draw();

            }
        }
    }
}
