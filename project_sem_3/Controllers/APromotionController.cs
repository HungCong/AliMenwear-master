using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using project_sem_3.Models;
using project_sem_3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace project_sem_3.Controllers
{
    public class APromotionController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        OrderDO orderDO = new OrderDO();
        ProductDO productDO = new ProductDO();
        MailUtil mailUtil = new MailUtil();
        Thread scheduler;

        [ResponseType(typeof(Ads))]
        public IHttpActionResult SendAll(Ads ads)
        {
            if (ads == null)
            {
                return Json(new
                {
                    success = false,
                    message = "id error"
                });
            }

            MailUtil mailUtil = new MailUtil();

            var listEmail = orderDO.GetListEmail();
            Product product = db.Products.Find(ads.ProductID);

            if (product == null)
            {
                return Json(new
                {
                    success = false,
                    message = "product not found"
                });
            }

            scheduler = new Thread(() => sendMailFT(listEmail, mailUtil.CreateBodyAds(product), "Alimenswear Ads"));
            scheduler.Start();

            return Json(new
            {
                success = true,
                message = "acction success"
            });
        }

        // api/APromotion/CreateSendAll
        [ResponseType(typeof(Ads))]
        public IHttpActionResult CreateSendAll(Ads ads)
        {
            if (ads == null)
            {
                return Json(new
                {
                    success = false,
                    message = "data invalid"
                });
            }

            var listEmail = orderDO.GetListEmail();
            var products = db.Products.Where(m => m.Status == EProductStatus.Active).Where(p => ads.ProductIDs.Contains(p.Id)).ToList();

            if (products == null)
            {
                return Json(new
                {
                    success = false,
                    message = "product not found"
                });
            }

            MailUtil mailUtil = new MailUtil();

            var emailBody = mailUtil.CreateBodyAds2(products, ads.Header);

            scheduler = new Thread(() => sendMailFT(listEmail, emailBody, ads.Subject));
            scheduler.Start();

            return Json(new
            {
                success = true,
                message = "acction success"
            });
        }

        // api/APromotion/Create
        [ResponseType(typeof(Ads))]
        public IHttpActionResult Create(Ads ads)
        {
            if (ads == null)
            {
                return Json(new
                {
                    success = false,
                    message = "data invalid"
                });
            }

            var listEmail = orderDO.CheckListEmail(ads.Emails);
            var products = db.Products.Where(m => m.Status == EProductStatus.Active).Where(p => ads.ProductIDs.Contains(p.Id)).ToList();

            if (products == null)
            {
                return Json(new
                {
                    success = false,
                    message = "product not found"
                });
            }

            MailUtil mailUtil = new MailUtil();

            var emailBody = mailUtil.CreateBodyAds2(products, ads.Header);

            scheduler = new Thread(() => sendMailFT(listEmail, emailBody, ads.Subject));
            scheduler.Start();

            return Json(new
            {
                success = true,
                message = "acction success"
            });
        }
        public void sendMailFT(List<string> listEmail, string body, string subject)
        {
            mailUtil.SendMails(listEmail, body, subject);
        }

        // api/APromotion/GetProduct
        [HttpPost]
        [ResponseType(typeof(SProducts))]
        public IHttpActionResult GetProduct(SProducts sProducts)
        {
            if (sProducts == null)
            {
                sProducts = new SProducts();
                sProducts.CategoryId = 0;
                sProducts.KeyWord = "";

            }

            var products = productDO.TakeProduct(sProducts.CategoryId, sProducts.KeyWord, sProducts.Page, sProducts.Size);
            var paginate = new Paginate()
            {
                CurrentPage = sProducts.Page,
                PageSize = sProducts.Size,
                TotalPage = productDO.GetTotalPage(sProducts.Size, sProducts.CategoryId, sProducts.KeyWord)
            };

            return Json(new
            {
                success = true,
                message = "acction success",
                data = products,
                paginate = paginate
            });
        }

        // api/APromotion/GetEmail
        [HttpPost]
        [ResponseType(typeof(SEmails))]
        public IHttpActionResult GetEmail(SEmails sEmails)
        {
            var emails = orderDO.TakeEmail(sEmails.Keyword, sEmails.Page, sEmails.Size);
            var paginate = new Paginate()
            {
                CurrentPage = sEmails.Page,
                PageSize = sEmails.Size,
                TotalPage = orderDO.GetTotalPageForEmail(sEmails.Size, sEmails.Keyword)
            };
            return Json(new
            {
                success = true,
                message = "acction success",
                data = emails,
                paginate
            });
        }
    }
}