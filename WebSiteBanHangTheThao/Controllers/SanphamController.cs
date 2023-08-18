using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangTheThao.Context;

namespace WebSiteBanHangTheThao.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        QLBanHangTheThaoEntities qLBanHangTheThaoEntities = new QLBanHangTheThaoEntities();
        public ActionResult Chitietsanpham(int id)
        {
            var ctProduct = qLBanHangTheThaoEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(ctProduct);
        }
       
    }
}