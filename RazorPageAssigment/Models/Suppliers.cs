using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RazorPageAssigment.Models
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Products = new HashSet<Products>();
        }

        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
