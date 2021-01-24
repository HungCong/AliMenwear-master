using Microsoft.Ajax.Utilities;
using project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_sem_3.Utils
{
    public class ProductDO
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Product> TakeProduct(int categoryId, string keyword, int page, int size)
        {
            var products = from m in db.Products select m;
            products = products.Where(m => m.Status == EProductStatus.Active).OrderBy(m => m.CreatedAt);

            if (categoryId != 0)
            {
                products = products.Where(m => m.CategoryId == categoryId);
            }

            if (!keyword.IsNullOrWhiteSpace())
            {
                products = products.Where(m => m.Name.ToLower().Contains(keyword.ToLower()));
            }

            if (page < 1)
            {
                page = 1;
            }

            if (size < 1)
            {
                size = 5;
            }

            int skipRows = (page - 1) * size;

            products = products.Skip(skipRows).Take(size);

            return products.ToList();
        }

        public int GetTotalPage(int Size, int categoryId, string keyword)
        {
            var products = from m in db.Products select m;
            products = products.Where(m => m.Status == EProductStatus.Active);

            if (categoryId != 0)
            {
                products = products.Where(m => m.CategoryId == categoryId);
            }

            if (!keyword.IsNullOrWhiteSpace())
            {
                products = products.Where(m => m.Name.ToLower().Contains(keyword.ToLower()));
            }


            int Total = products.Count();
            return (Total + Size - 1) / Size;
        }
    }
}