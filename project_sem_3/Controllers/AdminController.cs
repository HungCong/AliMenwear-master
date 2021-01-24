using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Ajax.Utilities;
using PagedList;
using ClosedXML.Excel;
using project_sem_3.Models;
using Order = project_sem_3.Models.Order;
using DataTable = System.Data.DataTable;
using System.IO;

namespace project_sem_3.Controllers
{
    //[Authorize(Roles = "Super,Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly string[] thumbnail;

        // GET: Admin
        public ActionResult Index()
        {
            List<double> area = new List<double>();

            for (int i = 1; i <= 5; i++)
            {
                var query = db.Orders.Where(x => x.CreatedAt.Month == i).OrderBy(x => x.CreatedAt).Select(g => g.TotalPrice);
                area.Add(query.Sum(items => items));
            };

            var orders = db.Orders.Include(o => o.Discount).Take(5);
            var totalUser = db.Users.Include(o => o.Id);

            ViewBag.AreaChart = area;
            ViewBag.EARNINGS = area.Last();
            ViewBag.Sales = area.Count();
            ViewBag.NewUser = totalUser.Count();
            return View(orders);
        }

        // Admin/Role
        public ActionResult Role()
        {
            return View("~/Views/AdminRole/Index.cshtml", db.IdentityRoles.ToList());
        }

        // Admin/User
        public ActionResult User()
        {
            return View("~/Views/AdminUser/Index.cshtml", db.Users.ToList());
        }

        // GET: Admin/UserCreate
        public ActionResult UserCreate()
        {
            return View("~/Views/AdminUser/Create.cshtml");
        }

        // POST: Admin/UserCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("User");
            }

            return View("~/Views/AdminUser/Create.cshtml", applicationUser);
        }

        // GET: Admin/UserEdit/5
        public ActionResult UserEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/AdminUser/Edit.cshtml", applicationUser);
        }

        // POST: Admin/UserEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("User");
            }
            return View("~/Views/AdminUser/Edit.cshtml", applicationUser);
        }

        // GET: Admin/UserDelete/5
        public ActionResult UserDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/AdminUser/Delete.cshtml", applicationUser);
        }

        // POST: Admin/UserDelete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult UserDeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("User");
        }

        // GET: Discounts
        public ActionResult Discount()
        {
            return View("~/Views/AdminDiscount/Index.cshtml", db.Discounts.ToList());
        }

        // GET: Discounts/Details/5
        public ActionResult DiscountDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/AdminDiscount/Details.cshtml", discount);
        }

        // GET: Discounts/Create
        public ActionResult DiscountCreate()
        {
            //Discount discount = new Discount();

            //discount.Status = EDiscountStatus.Active;

            return View("~/Views/AdminDiscount/Create.cshtml");
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DiscountCreate([Bind(Include = "Id,Code,Value,ExprirationDate,MinTotal,UseTime,Status,CreatedAt,UpdatedAt,DeletedAt")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                discount.CreatedAt = DateTime.Now;
                db.Discounts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Discount");
            }

            return View("~/Views/AdminDiscount/Create.cshtml", discount);
        }

        // GET: Discounts/Edit/5
        public ActionResult DiscountEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/AdminDiscount/Edit.cshtml", discount);

        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DiscountEdit([Bind(Include = "Id,Code,Value,ExprirationDate,MinTotal,UseTime,Status,CreatedAt,UpdatedAt,DeletedAt")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                discount.UpdatedAt = DateTime.Now;
                if (discount.Status == EDiscountStatus.Deactive)
                {
                    discount.DeletedAt = DateTime.Now;
                }
                else
                {
                    discount.DeletedAt = null;
                }
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Discount");
            }
            return View("~/Views/AdminDiscount/Edit.cshtml", discount);
        }

        // GET: Discounts/Delete/5
        public ActionResult DiscountDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            discount.Status = EDiscountStatus.Deactive;
            discount.DeletedAt = DateTime.Now;
            db.Entry(discount).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Discount");
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DiscountDeleteConfirmed(int id)
        {
            Discount discount = db.Discounts.Find(id);
            discount.Status = EDiscountStatus.Deactive;
            db.Entry(discount).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Discount");
        }

        // GET: Discounts/Restore/5
        public ActionResult DiscountRestore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            discount.Status = EDiscountStatus.Active;
            discount.UpdatedAt = DateTime.Now;
            discount.DeletedAt = null;
            db.Entry(discount).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Discount");
        }

        // GET: Admin/IndexProduct
        public ActionResult Product(string sortOrder, string currentFilter, string searchString, int? status, int? page, DateTime? startDate, DateTime? endDate)
        {
            //var productModels = db.ProductModels.Include(p => p.Category).Include(p => p.CreatedBy).Include(p => p.DeletedBy).Include(p => p.UpdatedBy);
            //return View("~/Views/AdminProduct/Index.cshtml", productModels.ToList());

            ViewBag.CurrentSort = sortOrder;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.statusParm = status;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.CreatedAtSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var product = from p in db.Products select p;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(p => p.Name.Contains(searchString));
            }

            if (startDate != null || endDate != null)
            {
                var start = startDate.GetValueOrDefault().Date;
                start = start.Date + new TimeSpan(0, 0, 0);
                var end = endDate.GetValueOrDefault().Date;
                end = end.Date + new TimeSpan(23, 59, 59);
                product = product.Where(p => p.CreatedAt >= start && p.CreatedAt <= end);
            }

            switch (status)
            {
                case 2:
                    product = product.Where(p => p.Status == EProductStatus.Active);
                    break;
                case 3:
                    product = product.Where(p => p.Status == EProductStatus.Deactive);
                    break;
                case 1:
                    product = product.Where(p => p.Status == EProductStatus.Deleted);
                    break;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    product = product.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(p => p.Price);
                    break;
                case "Date":
                    product = product.OrderBy(p => p.CreatedAt);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    product = product.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View("~/Views/AdminProduct/Index.cshtml", product.ToPagedList(pageNumber, pageSize));
        }

        // Admin/ProductCreate
        public ActionResult ProductCreate()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View("~/Views/AdminProduct/Create.cshtml");
        }

        // Admin/ProductCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate([Bind(Include = "Id,Name,Price,Discount,Description,CategoryId,Thumbnails,CreatedAt,UpdatedAt,DeletedAt")] Product product, string[] thumbnails)
        {
            if (ModelState.IsValid)
            {
                if (thumbnails != null && thumbnails.Length > 0)
                {
                    product.Thumbnails = string.Join(",", thumbnails);
                }
                product.CreatedAt = DateTime.Now;
                product.Status = EProductStatus.Active;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Product");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View("~/Views/AdminProduct/Create.cshtml", product);
        }

        // Admin/ProductDetailCreate/5
        public ActionResult ProductDetailCreate(int id)
        {
            var productDetails = new ProductDetail()
            {
                ProductId = id,
            };
            return View("~/Views/AdminProduct/CreateDetails.cshtml", productDetails);
        }

        // POST: Admin/ProductDetailCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDetailCreate([Bind(Include = "Id,Color,Size,Quantity,ProductId,Thumbnails")] ProductDetail productDetail, string[] thumbnails)
        {
            if (ModelState.IsValid)
            {
                if (thumbnails != null && thumbnails.Length > 0)
                {
                    productDetail.Thumbnails = string.Join(",", thumbnails);
                }
                productDetail.Status = EProductStatus.Active;

                Product product = db.Products.Find(productDetail.ProductId);
                if (product == null)
                {
                    return HttpNotFound();
                }
                productDetail.Product = product;
                db.ProductDetails.Add(productDetail);
                db.SaveChanges();
                return RedirectToAction("Product");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productDetail.ProductId);
            return View("~/Views/AdminProduct/Edit.cshtml", productDetail);
        }

        // Admin/ProductEdit/5
        public ActionResult ProductEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View("~/Views/AdminProduct/Edit.cshtml", product);
        }

        // POST: Admin/ProductEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit([Bind(Include = "Id,Name,Price,Discount,Description,CategoryId,Status,Thumbnails,CreatedAt,UpdatedAt,DeletedAt")] Product product, string[] thumbnails)
        {
            if (ModelState.IsValid)
            {
                if (thumbnails != null && thumbnails.Length > 0)
                {
                    product.Thumbnails = string.Join(",", thumbnails);
                }
                if (product.Status == EProductStatus.Deleted)
                {
                    product.UpdatedAt = null;
                    product.DeletedAt = DateTime.Now;
                }
                else
                {
                    product.UpdatedAt = DateTime.Now;
                    product.DeletedAt = null;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Product");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View("~/Views/AdminProduct/Edit.cshtml", product);
        }

        // GET: Admin/ProductDelete/5
        public ActionResult ProductDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            product.Status = EProductStatus.Deleted;
            product.DeletedAt = DateTime.Now;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Product");
        }

        // GET: Admin/ProductDetail/5        
        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            List<ProductDetail> productDetail = db.ProductDetails.Where(m => m.ProductId == id).ToList();
            if (product == null)
            {
                return HttpNotFound();
            }
            if (productDetail == null)
            {
                productDetail = new List<ProductDetail>();
            }
            var ProductDetailViewModel = new ProductDetailViewModel()
            {
                Product = product,
                ProductDetails = productDetail
            };
            return View("~/Views/AdminProduct/Details.cshtml", ProductDetailViewModel);
        }

        // GET: Admin/ProductDetailEdit/5
        public ActionResult ProductDetailEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = db.ProductDetails.Find(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productDetail.ProductId);
            return View("~/Views/AdminProduct/EditDetails.cshtml", productDetail);
        }

        // POST: Admin/ProductDetailEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDetailEdit([Bind(Include = "Id,Color,Size,Quantity,Status,ProductId,Thumbnails")] ProductDetail productDetail, string[] thumbnails)
        {
            if (ModelState.IsValid)
            {
                if (thumbnails != null && thumbnails.Length > 0)
                {
                    productDetail.Thumbnails = string.Join(",", thumbnails);
                }
                db.Entry(productDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Product");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productDetail.ProductId);
            return View("~/Views/AdminProduct/EditDetails.cshtml", productDetail);
        }

        // GET: Admin/ProductDetailDelete/5
        public ActionResult ProductDetailDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDetail productDetail = db.ProductDetails.Find(id);
            if (productDetail == null)
            {
                return HttpNotFound();
            }
            productDetail.Status = EProductStatus.Deleted;
            db.Entry(productDetail).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Product");
        }

        // GET: Admin/Category
        public ActionResult Category()
        {
            var category = db.Categories.ToList();
            return View("~/Views/AdminCategory/Index.cshtml", category);
        }

        // GET: Category/Details/5
        public ActionResult CategoryDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/AdminCategory/Details.cshtml", category);
        }

        // Admin/CreateCategory
        public ActionResult CategoryCreate()
        {
            return View("~/Views/AdminCategory/Create.cshtml");
        }

        // POST: Admin/CreateCategory
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Status = ECategoryStatus.Active;
                category.CreatedAt = DateTime.Now;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Category");
            }

            return View("~/Views/AdminCategory/Create.cshtml", category);
        }

        // Admin/EditCategory/5
        public ActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/AdminCategory/Edit.cshtml", category);
        }

        // POST: Admin/EditCategory/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit([Bind(Include = "Id,Name,Status,CreatedAt,UpdatedAt,DeletedAt")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.UpdatedAt = DateTime.Now;
                if (category.Status == ECategoryStatus.Deactive)
                {
                    category.DeletedAt = DateTime.Now;
                }
                else
                {
                    category.DeletedAt = null;
                }
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Category");
            }
            return View("~/Views/AdminCategory/Edit.cshtml", category);
        }

        // GET: Admin/CategoryDelete/5
        public ActionResult CategoryDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            category.Status = ECategoryStatus.Deactive;
            category.DeletedAt = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Category");
        }

        // GET: Admin/CategoryRestore/5
        public ActionResult CategoryRestore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            category.Status = ECategoryStatus.Active;
            category.UpdatedAt = DateTime.Now;
            category.DeletedAt = null;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Category");
        }

        // Admin/Order
        public ActionResult Order(DateTime? startDate, DateTime? endDate, int? month)
        {
            var orders = db.Orders.Include(o => o.Discount);
            orders = orders.OrderBy(o => o.CreatedAt);

            if (month != null)
            {
                orders = orders.Where(o => o.CreatedAt.Month == month);
            }
            else
            {
                orders = orders.Include(o => o.Discount);
            }


            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            if (startDate != null || endDate != null)
            {
                var start = startDate.GetValueOrDefault().Date;
                start = start.Date + new TimeSpan(0, 0, 0);
                var end = endDate.GetValueOrDefault().Date;
                end = end.Date + new TimeSpan(23, 59, 59);
                orders = orders.Where(o => o.CreatedAt >= start && o.CreatedAt <= end);
            }

            return View("~/Views/AdminOrder/Index.cshtml", orders.ToList());
        }

        // GET: Admin/OrderDetails/5
        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/AdminOrder/Details.cshtml", orders);
        }

        // GET: AdminOrder/Create
        public ActionResult OrderCreate()
        {
            ViewBag.DiscountId = new SelectList(db.Discounts, "Id", "Code");
            return View("~/Views/AdminOrder/Create.cshtml");
        }

        // POST: AdminOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCreate([Bind(Include = "Id,CustomerName,CustomerPhone,CustomerEmail,CustomerAddress,PaymentMethod,TotalPrice,DiscountId,PaymentStatus")] Order orders)
        {
            if (ModelState.IsValid)
            {
                orders.Status = EOrderStatus.Received;
                orders.CreatedAt = DateTime.Now;
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Order");
            }

            ViewBag.DiscountId = new SelectList(db.Discounts, "Id", "Code", orders.DiscountId);
            return View("~/Views/AdminOrder/Create.cshtml", orders);
        }

        // GET: AdminOrder/Edit/5
        public ActionResult OrderEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }

            ViewBag.DiscountId = new SelectList(db.Discounts, "Id", "Code", orders.DiscountId);
            return View("~/Views/AdminOrder/Edit.cshtml", orders);
        }

        // POST: AdminOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderEdit([Bind(Include = "Id,CustomerName,CustomerPhone,CustomerEmail,CustomerAddress,PaymentMethod,TotalPrice,DiscountId,Status,PaymentStatus,CreatedAt")] Order orders)
        {
            if (ModelState.IsValid)
            {
                orders.UpdatedAt = DateTime.Now;
                if (orders.Status == EOrderStatus.Cancelled)
                {
                    orders.DeletedAt = DateTime.Now;
                }
                else
                {
                    orders.DeletedAt = null;
                }
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Order");
            }

            ViewBag.DiscountId = new SelectList(db.Discounts, "Id", "Code", orders.DiscountId);
            return View("~/Views/AdminOrder/Edit.cshtml", orders);
        }

        // GET: AdminOrder/Delete/5
        public ActionResult OrderDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            orders.Status = EOrderStatus.Cancelled;
            orders.DeletedAt = DateTime.Now;
            db.Entry(orders).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Order");
        }

        // GET: Admin/OrderRestore/5
        public ActionResult OrderRestore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            orders.Status = EOrderStatus.Received;
            orders.UpdatedAt = DateTime.Now;
            orders.DeletedAt = null;
            db.Entry(orders).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Order");
        }

        // GET: Admin/Home
        public ActionResult Home()
        {
            return View();
        }

        // GET: Admin/Error404
        public ActionResult Error404()
        {
            return View();
        }

        // GET: Admin/Alerts
        public ActionResult Alerts()
        {
            return View();
        }

        // GET: Admin/Blank
        public ActionResult Blank()
        {
            return View();
        }

        // GET: Admin/Buttons
        public ActionResult Buttons()
        {
            return View();
        }

        // GET: Admin/Charts
        public ActionResult Charts()
        {
            return View();
        }

        // GET: Admin/Copycontent
        public ActionResult Copycontent()
        {
            return View();
        }

        // GET: Admin/Datatables
        public ActionResult Datatables()
        {
            return View();
        }

        // GET: Admin/Dropdowns
        public ActionResult Dropdowns()
        {
            return View();
        }

        // GET: Admin/Forms
        public ActionResult Forms()
        {
            return View();
        }

        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Admin/Modals
        public ActionResult Modals()
        {
            return View();
        }

        // GET: Admin/Popovers
        public ActionResult Popovers()
        {
            return View();
        }

        // GET: Admin/ProgressBar
        public ActionResult ProgressBar()
        {
            return View();
        }

        // GET: Admin/Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Admin/SimpleTables
        public ActionResult SimpleTables()
        {
            return View();
        }

        // GET: Admin/UiColors
        public ActionResult UiColors()
        {
            return View();
        }

        [HttpPost]
        public FileResult ExportProduct()
        {
            var result = from p in db.Products
                         join pd in db.ProductDetails on p.Id equals pd.ProductId
                         select new
                         {
                             p.Name,
                             p.Category,
                             p.Price,
                             p.Discount,
                             pd.Color,
                             pd.Size,
                             pd.Quantity
                         };
            DataTable dt = new DataTable("Products");
            dt.Columns.AddRange(new DataColumn[8] {
                                            new DataColumn("No"),
                                            new DataColumn("Name"),
                                            new DataColumn("Category"),
                                            new DataColumn("Price"),
                                            new DataColumn("Discount"),
                                            new DataColumn("Color"),
                                            new DataColumn("Size"),
                                            new DataColumn("Quantity") });
            int index = 1;
            foreach (var item in result)
            {
                dt.Rows.Add(
                            index++,
                            item.Name,
                            item.Category.Name,
                            item.Price,
                            item.Discount,
                            item.Color,
                            item.Size,
                            item.Quantity);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Alimenswear_Products.xlsx");
                }
            }
        }

        public FileResult ExportOrder()
        {
            var result = from o in db.Orders
                         join od in db.OrderDetails on o.Id equals od.OrderId
                         join pd in db.ProductDetails on od.ProductDetailId equals pd.Id
                         select new
                         {
                             o.Id,
                             o.CustomerName,
                             o.CustomerPhone,
                             o.CustomerEmail,
                             o.CustomerAddress,
                             od.ProductDetail,
                             pd.Product,
                             od.Quantity,
                             o.Discount.Code,
                             o.TotalPrice,
                             o.PaymentMethod,
                             o.Status,
                             o.CreatedAt,
                         };

            DataTable dtOrder = new DataTable("Orders");
            dtOrder.Columns.AddRange(new DataColumn[10] {
                                            new DataColumn("Id"),
                                            new DataColumn("Date"),
                                            new DataColumn("Name"),
                                            new DataColumn("Phone"),
                                            new DataColumn("Email"),
                                            new DataColumn("Address"),
                                            new DataColumn("Discount"),
                                            new DataColumn("Total"),
                                            new DataColumn("Payment"),
                                            new DataColumn("Status")
                                            });

            DataTable dtOrderDetails = new DataTable("OrderDetails");
            dtOrderDetails.Columns.AddRange(new DataColumn[7] {
                                            new DataColumn("OrderId"),
                                            new DataColumn("Product"),
                                            new DataColumn("Color"),
                                            new DataColumn("Size"),
                                            new DataColumn("Price"),
                                            new DataColumn("Discount"),
                                            new DataColumn("Quantity"),
            });

            foreach (var item in result)
            {
                dtOrder.Rows.Add(
                            item.Id,
                            item.CreatedAt,
                            item.CustomerName,
                            item.CustomerPhone,
                            item.CustomerEmail,
                            item.CustomerAddress,
                            item.Code,
                            item.TotalPrice,
                            item.PaymentMethod,
                            item.Status
                           );

                dtOrderDetails.Rows.Add(
                            item.Id,
                            item.Product.Name,
                            item.ProductDetail.Color,
                            item.ProductDetail.Size,
                            item.Product.Price,
                            item.Product.Discount,
                            item.Quantity
                            );
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtOrder);
                wb.Worksheets.Add(dtOrderDetails);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Alimenswear_Order.xlsx");
                }
            }
        }

        // GET: Admin/Promotion
        public ActionResult Promotion(string sortOrder, string currentFilter, string searchString, int? page, DateTime? startDate, DateTime? endDate)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;            
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.CreatedAtSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var product = from p in db.Products select p;
            product = product.Where(m => m.Status == EProductStatus.Active);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(p => p.Name.Contains(searchString));
            }

            if (startDate != null || endDate != null)
            {
                var start = startDate.GetValueOrDefault().Date;
                start = start.Date + new TimeSpan(0, 0, 0);
                var end = endDate.GetValueOrDefault().Date;
                end = end.Date + new TimeSpan(23, 59, 59);
                product = product.Where(p => p.CreatedAt >= start && p.CreatedAt <= end);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    product = product.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(p => p.Price);
                    break;
                case "Date":
                    product = product.OrderBy(p => p.CreatedAt);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    product = product.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View("~/Views/AdminPromotion/Index.cshtml", product.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult PromotionCreate()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View("~/Views/AdminPromotion/Create.cshtml");
        }
    }
}
