using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangTheThao.Context;

namespace WebSiteBanHangTheThao.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        QLBanHangTheThaoEntities qLBanHangTheThaoEntities = new QLBanHangTheThaoEntities();
        // GET: Admin/SanPham
        public ActionResult Sanpham(string Timkiem,string currentFilter)
        {
            var sp = new List<Product>();
            if (Timkiem != null)
            {

            }
            else
            {
                Timkiem = currentFilter;
            }
            if (!string.IsNullOrEmpty(Timkiem))
            {
                 sp = qLBanHangTheThaoEntities.Products.Where(n => n.Name.Contains(Timkiem)).ToList();

            }
            else
            {
                sp = qLBanHangTheThaoEntities.Products.ToList();
            }
            return View(sp);
        }

        public ActionResult Chitietsanpham(int id)
        {
            var dtSanPham = qLBanHangTheThaoEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(dtSanPham);
        }

        [HttpGet]
        public ActionResult Taosanpham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Taosanpham(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(product.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(product.ImageUpLoad.FileName);
                        fileName = fileName + extension;
                        product.Avata = fileName;
                        fileName=(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                        product.ImageUpLoad.SaveAs(fileName);
                    }
                    qLBanHangTheThaoEntities.Products.Add(product);
                    qLBanHangTheThaoEntities.SaveChanges();
                    return RedirectToAction("Sanpham");
                }
                catch
                {
                    return View();
                }
            }
            return View(product);

        }
        [HttpGet]
        public ActionResult Xoasanpham(int id)
        {
            var dtSanPham = qLBanHangTheThaoEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(dtSanPham);
        }
        [HttpPost]
        public ActionResult Xoasanpham(Product product)
        {
            var dtSanPham = qLBanHangTheThaoEntities.Products.Where(n => n.Id == product.Id).FirstOrDefault();
            qLBanHangTheThaoEntities.Products.Remove(dtSanPham);
            qLBanHangTheThaoEntities.SaveChanges();
            return RedirectToAction("SanPham");
        }

        [HttpGet]
        public ActionResult Chinhsua(int id)
        {
            var dtSanPham = qLBanHangTheThaoEntities.Products.Where(n => n.Id == id).FirstOrDefault();
            return View(dtSanPham);
        }

        [HttpPost]
        public ActionResult Chinhsua(int id,Product product)
        {
            if (product.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(product.ImageUpLoad.FileName);
                string extension = Path.GetExtension(product.ImageUpLoad.FileName);
                fileName = fileName + extension;
                product.Avata = fileName;
                product.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            qLBanHangTheThaoEntities.Entry(product).State =EntityState.Modified;
            qLBanHangTheThaoEntities.SaveChanges();
            return RedirectToAction("Sanpham");
        }   
    }
}