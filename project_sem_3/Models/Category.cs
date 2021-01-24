using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_sem_3.Models
{
    [Table("Categories")]
    public class Category : BaseDate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public ECategoryStatus Status { get; set; }
    }

    public enum ECategoryStatus
    {
        Active = 1,
        Deactive = 2
    }
}