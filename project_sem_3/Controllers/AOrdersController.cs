using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using project_sem_3.Models;

namespace project_sem_3.Controllers
{
    public class AOrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // todo validate request
        // validate data on db
        // response if diff

        // POST: api/AOrders
        [ResponseType(typeof(Cart))]
        public IHttpActionResult PostOrder(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DbContextTransaction dbTran = db.Database.BeginTransaction();
            // todo trungnq valideate
            Order order = cart.order;
            order.CreatedAt = DateTime.Now;
            order.Status = EOrderStatus.Received;
            var details = cart.details;

            try
            {
                db.Orders.Add(order);
                db.SaveChanges();

                for (int i = 0; i < details.Count; i++)
                {
                    OrderDetail detail = details[i];
                    detail.OrderId = order.Id;

                    var rs = db.ProductDetails.Find(detail.ProductDetailId);
                    if (rs.Quantity < detail.Quantity)
                    {
                        // handle err
                        // return update
                        dbTran.Rollback();
                        var data = new
                        {
                            productDetailId = detail.ProductDetailId,
                        };
                        return Json(new
                        {
                            success = false,
                            message = "Product detail quantity invalid",
                            data
                        });
                    }
                    else
                    {
                        rs.Quantity -= detail.Quantity;
                        db.Entry(rs).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    // todo trungnq get data from db and check diff
                    // order.AddOrderDetail(detail);
                    db.OrderDetails.Add(detail);
                    db.SaveChanges();
                }

                dbTran.Commit();
                return Json(new
                {
                    success = true,
                    message = "Add success"
                });
            }
            catch (Exception e)
            {
                dbTran.Rollback();
                return Json(new
                {
                    success = false,
                    message = "Internal err",
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}