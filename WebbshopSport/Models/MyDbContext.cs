using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbshopSport.Models
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Size> sizes { get; set; }
        public DbSet<Supplier> suppliers { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=tcp:emildb.database.windows.net,1433;Initial Catalog=emilsdb;Persist Security Info=False;User ID=emilb123;Password=Emiltheo!123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Webbshoppen;Trusted_Connection=True; TrustServerCertificate=True;");
        }
    }
}
