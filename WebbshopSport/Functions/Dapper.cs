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
using Microsoft.Extensions.Options;



namespace WebbshopSport.Functions
{
    internal class Dapper
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog=WebbshopSport; persist security info=True; " +
            "Integrated Security=True;";
        //static string connString = "Server=tcp:emildb.database.windows.net,1433;Initial Catalog=emilsdb;Persist Security Info=False;User ID=emilb123;Password=Emiltheo!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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

        
    }
}
