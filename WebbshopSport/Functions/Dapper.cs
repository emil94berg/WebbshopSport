using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace WebbshopSport.Functions
{
    internal class Dapper
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog=WebbshopSport; persist security info=True; " +
            "Integrated Security=True;";

        public static List<Models.Product> GetWantedProduct(string search)
        {
            string sql = $"SELECT * FROM Products WHERE Name LIKE '%{search}%'";

            List<Models.Product> product = new List<Models.Product>();

            using (var connection = new SqlConnection(connString))
            {
                product = connection.Query<Models.Product>(sql).ToList();
            }
            return product;
        }
    }
}
