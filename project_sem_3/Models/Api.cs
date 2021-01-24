using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_sem_3.Models
{
    public class Api
    {
    }

    public class Cart
    {
        public Order order { get; set; }
        public List<OrderDetail> details { get; set; }
    }

    public class Ads
    {
        public int ProductID { get; set; }
        public int[] ProductIDs { get; set; }
        public string[] Emails { get; set; }
        public string Subject { get; set; }
        public string Header { get; set; }
    }
    
    public class SearchString
    {
        public string KeyWord { get; set; }
    }

    public class SProducts
    {
        public int CategoryId { get; set; }
        public string KeyWord { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }

    public class SEmails
    {
        public string Keyword { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }

    public class Paginate
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }

    }
}