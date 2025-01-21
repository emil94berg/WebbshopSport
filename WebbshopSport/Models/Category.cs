using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbshopSport.Models
{
    internal class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int ProductId { get; set; }
        //public  virtual Product Product { get; set; } // går det att ta väck kategorier som är kopplade till produkter?
        public virtual ICollection<Product> Products { get; set; }
    }
}
