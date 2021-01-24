using PagedList;
using project_sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace project_sem_3.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var products = from p in db.Products select p;
            products = products.Where(m => m.Status == EProductStatus.Active).OrderByDescending(p => p.CreatedAt).Take(4);
             return View(products.ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}