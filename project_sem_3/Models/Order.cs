using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_sem_3.Models
{
    [Table("Orders")]
    public class Order : BaseDate
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Phone")]
        public string CustomerPhone { get; set; }
        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; }
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Payment Method")]
        public EPaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }
        [Display(Name = "Discount Id")]
        public int? DiscountId { get; set; }
        [Display(Name = "Discount")]
        [ForeignKey("DiscountId")]
        public virtual Discount Discount { get; set; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
        [Display(Name = "Deleted At")]
        public DateTime? DeletedAt { get; set; }
        [Display(Name = "Status")]
        public EOrderStatus Status { get; set; }
        [Display(Name = "Order Details")]
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if (this.OrderDetails == null)
            {
                this.OrderDetails = new List<OrderDetail>();
            }
            this.OrderDetails.Add(orderDetail);
            this.TotalPrice += orderDetail.UnitPrice * orderDetail.Quantity;
        }
    }
    public enum EPaymentMethod
    {
        Cash = 1,       // Tiền mặt
        Paypal = 2,     // Paypal
    }
    public enum EOrderStatus
    {
        Received = 1,   // Đã tiếp nhận
        Processing = 2, // Đang xử lý
        Shipping = 3,   // Đang giao
        Delivered = 4,  // Đã giao
        Cancelled = 5,  // Hủy bỏ
    }
   
}