using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbshopSport.Models
{
    internal class ShoppingCart
    {
        public int Id { get; set; }
        public int TotalSum { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
