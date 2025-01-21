using Microsoft.IdentityModel.Tokens;
using WebbshopSport.GraphWindow;
using WebbshopSport.Models;

namespace WebbshopSport
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //using (var db = new MyDbContext())
            //{
            //    var testList = 
            //}


            //List<string> cartText = new List<string> { "1 st Blå byxor", "2 st Grön tröja", "1 st Röd skjorta", "Tryck X för att checka ut" };
            //var windowCart = new Window("Din varukorg", 0, 0, cartText);
            //windowCart.Draw();

            //Functions.Admin.AddCategory();
            //Functions.Admin.AddSupplier();
            Functions.Admin.AddProduct();
            //Functions.Admin.AddSize();
            //Functions.Admin.ChangeCategory();
            //Functions.Admin.ChangeSupplier();
            //Functions.Admin.DeleteSupplier();
            //Functions.Admin.DeleteProduct();





        }
    }
}
