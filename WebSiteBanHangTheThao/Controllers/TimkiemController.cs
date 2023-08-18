using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangTheThao.Context;

namespace WebSiteBanHangTheThao.Controllers
{
    public class TimkiemController : Controller
    {
        QLBanHangTheThaoEntities qLBanHangTheThaoEntities = new QLBanHangTheThaoEntities();
        // GET: Timkiem
        public ActionResult KQTimKiem(string sTuKhoa)
        {
            var lstSP = qLBanHangTheThaoEntities.Products.Where(n => n.Name.Contains(sTuKhoa));
            return View(lstSP.OrderBy(n =>n.Name));
        }
    }
}