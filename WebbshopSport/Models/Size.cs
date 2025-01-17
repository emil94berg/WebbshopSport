﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbshopSport.Models
{
    internal class Size
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
