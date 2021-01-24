using Microsoft.Ajax.Utilities;
using project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_sem_3.Utils
{
    public class OrderDO
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<string> GetListEmail()
        {
            return db.Orders.DistinctBy(m => m.CustomerEmail).Select(m => m.CustomerEmail).ToList();
        }

        public List<string> CheckListEmail(string[] list)
        {
            return list.ToList();
        }

        public List<string> GetListEmailByKeyWord(string keyword)
        {
            var emails = from m in db.Orders
                         where m.CustomerEmail.ToLower().Contains(keyword.ToLower())
                         select m;
            return emails.DistinctBy(m => m.CustomerEmail).Select(m => m.CustomerEmail).ToList();
        }

        public List<string> TakeEmail(string keyword, int page, int size)
        {
            var orders = from m in db.Orders select m;
            orders = orders.OrderBy(m => m.CreatedAt);

            if (!keyword.IsNullOrWhiteSpace())
            {
                orders = orders.Where(m => m.CustomerEmail.ToLower().Contains(keyword.ToLower()));
            }

            var emails = orders.DistinctBy(m => m.CustomerEmail).Select(m => m.CustomerEmail);

            if (page < 1)
            {
                page = 1;
            }

            if (size < 1)
            {
                size = 5;
            }

            int skipRows = (page - 1) * size;
            emails = emails.Skip(skipRows).Take(size);
            return emails.ToList();
        }

        public int GetTotalPageForEmail(int Size, string keyword)
        {
            var orders = from m in db.Orders select m;
            var emails = orders.DistinctBy(m => m.CustomerEmail).Select(m => m.CustomerEmail);

            if (!keyword.IsNullOrWhiteSpace())
            {
                emails = emails.Where(m => m.ToLower().Contains(keyword.ToLower()));
            }

            int Total = emails.Count();
            return (Total + Size - 1) / Size;
        }
    }
}