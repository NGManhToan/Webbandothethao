using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangTheThao.Context;
using WebSiteBanHangTheThao.Models;

namespace WebSiteBanHangTheThao.Controllers
{
    public class ThanhtoanController : Controller
    {
        QLBanHangTheThaoEntities qLBanHangTheThaoEntities = new QLBanHangTheThaoEntities();
        // GET: Thanhtoan
        public ActionResult Thanhtoan()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Dangnhap", "Home");
            }
            else
            {
                var listgiohang = (List<CartModel>)Session["cart"];
                Order order = new Order();
                order.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                order.UserId = int.Parse(Session["idUser"].ToString());
                order.CreatedOnUtc = DateTime.Now.ToString();
                order.Status = 1.ToString();
                qLBanHangTheThaoEntities.Orders.Add(order);
                qLBanHangTheThaoEntities.SaveChanges();
                int orderId = order.Id;
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (var item in listgiohang)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.Quantily = item.Quantity;
                    detail.OrderId = orderId;
                    detail.ProductId = item.Product.Id;
                    orderDetails.Add(detail);
                }
                qLBanHangTheThaoEntities.OrderDetails.AddRange(orderDetails);
                qLBanHangTheThaoEntities.SaveChanges();
                return View();
            }
        }
    } 
}