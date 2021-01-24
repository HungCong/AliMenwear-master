using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_sem_3.Models
{
    [Table("ProductDetails")]
    public class ProductDetail : BasePhoto
    {
        [Key]
        public int Id { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public EProductStatus Status { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Product")]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}