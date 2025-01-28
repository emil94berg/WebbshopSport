using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WebbshopSport.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace WebbshopSport.Functions
{
    internal class Dapper
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog=WebbshopSport; persist security info=True; " +
            "Integrated Security=True;";

        public static async Task<List<Models.Product>> GetWantedProduct(string search)
        {
            string sql = $"SELECT * FROM Products WHERE Name LIKE '%{search}%'";

            List<Models.Product> product = new List<Models.Product>();

            using (var connection = new SqlConnection(connString))
            {
                product = (await connection.QueryAsync<Models.Product>(sql)).ToList();

            }
            return product;
        }

        //public static async Task<List<Models.Car>> GetCarsAsync(MyDbContext db)
        //{
        //    Console.WriteLine("Hämtar från databas");
        //    var cars = await db.Cars.ToListAsync();  //Kräver EntityFrameWorkCore
        //    Console.WriteLine("Hämtat från databas");
        //    return cars;

        //}
    }
}
