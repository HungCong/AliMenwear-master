using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_sem_3.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Order Id")]
        public int OrderId { get; set; }
        [Display(Name = "Order")]
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [Display(Name = "Product Detail Id")]
        public int ProductDetailId { get; set; }
        [Display(Name = "Product Detail")]
        [ForeignKey("ProductDetailId")]
        public virtual ProductDetail ProductDetail { get; set; }
        [Display(Name = "Unit Price")]
        public double UnitPrice { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Discount")]
        public double Discount { get; set; }
    }
}