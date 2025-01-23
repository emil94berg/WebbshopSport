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
        public static void ShowCategory(int answear)
        {
            using (var db = new MyDbContext())
            {
                var showCategory = db.Products.Where(x => x.CategoryId == answear).Select(x => $"{x.Id} - {x.Name} - {x.Price}").ToList();
                foreach (var item in showCategory)
                {
                    Console.WriteLine(item);
                    
                }
                int ammount = showCategory.Count();
                Console.WriteLine("Choose product: ");
                int productAnswear = int.Parse(Console.ReadLine());
                var nameOfProduct = db.Products.Where(x => x.Id == productAnswear).Select(x => $"{x.Name}").SingleOrDefault();
                var showProduct = db.Products.Where(x => x.Id == productAnswear).Select(x => $"Description : {x.Description}").ToList();
                var choosenProductWindow = new Window(nameOfProduct, 0, ammount, showProduct);
                choosenProductWindow.Draw();
                


            }
        }
    }
}
