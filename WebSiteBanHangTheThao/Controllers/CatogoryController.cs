using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebSiteBanHangTheThao.Context;

namespace WebSiteBanHangTheThao.Controllers
{
    public class CatogoryController : Controller
    {
        // GET: Catogory
        QLBanHangTheThaoEntities qLBanHangTheThaoEntities = new QLBanHangTheThaoEntities();

        public ActionResult Loaisanpham(int id)
        {
            var lstcatagory = qLBanHangTheThaoEntities.Categories.ToList();
            return View(lstcatagory);
        }

        public ActionResult Danhsachloaisanpham(int id)
        {
           
            var lisProducttoname = qLBanHangTheThaoEntities.Products.Where(n => n.CategoryId == id).ToList();
            return View(lisProducttoname);
        }
    }
}