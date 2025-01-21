using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbshopSport.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; } 
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool Choosen { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }  
        public int Ammount { get; set; }
        public virtual ICollection<Size>? Sizes { get; set; }

    }
}
