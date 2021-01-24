using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_sem_3.Models
{
    [Table("Discounts")]
    public class Discount : BaseDate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [Range(0.1, 0.9)]
        public double Value { get; set; }
        [Display(Name = "Expriration Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExprirationDate { get; set; }
        [Required]
        public double MinTotal { get; set; }
        [Required]
        public double UseTime { get; set; }
        [Required]
        public EDiscountStatus Status { get; set; }
    }

    public enum EDiscountStatus
    {
        Active = 1,
        Deactive = 2
    }
}