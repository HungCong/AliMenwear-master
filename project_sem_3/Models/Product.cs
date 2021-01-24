using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_sem_3.Models
{
    [Table("Products")]
    public class Product : BasePhotoDate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [Display(Name = "Status")]
        public EProductStatus Status { get; set; }
    }
    public enum EProductStatus
    {
        Deleted = 1,
        Active = 2,
        Deactive = 3
    }
}