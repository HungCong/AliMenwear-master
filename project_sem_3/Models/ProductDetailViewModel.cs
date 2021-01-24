using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_sem_3.Models
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
        public List<Product> Products { get; set; }
    }

}