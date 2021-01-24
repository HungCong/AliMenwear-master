using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using project_sem_3.Models;

namespace project_sem_3.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page, int? category)
        {
            var product = from p in db.Products select p;
            product = product.Where(m => m.Status == EProductStatus.Active).OrderByDescending(p => p.CreatedAt);

            if (category != null)
            {
                var tmp = db.Categories.Where(m => m.Id == category && m.Status == ECategoryStatus.Active);
                if (tmp != null)
                {
                    product = product.Where(m => m.CategoryId == category);
                    ViewBag.CurrentCategoryId = category;
                }
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.Categories = db.Categories.Where(m => m.Status == ECategoryStatus.Active).ToList();
            return View(product.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Product(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productSingle = new ProductDetailViewModel();

            Product product = db.Products.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }

            List<ProductDetail> productDetails = db.ProductDetails.Where(pd => pd.ProductId == Id.Value).ToList();
            productSingle.Product = product;
            productSingle.ProductDetails = productDetails;

            ViewBag.totalQuantity = productDetails.Where(m => m.Status == EProductStatus.Active).Sum(m => m.Quantity);

            List<Product> relatedProducts = db.Products.Where(rp => rp.CategoryId == product.CategoryId && rp.Id != Id ).Take(4).ToList();
            productSingle.Products = relatedProducts;

            return View(productSingle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
