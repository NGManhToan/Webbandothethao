using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangTheThao.Context;

namespace WebSiteBanHangTheThao.Areas.Admin.Controllers
{
    public class DangNhapForAdminController : Controller
    {
        // GET: Admin/DangNhapForAdmin
        QLBanHangTheThaoEntities qLBanHangTheThaoEntities = new QLBanHangTheThaoEntities();
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangnhap(string email, string password)
        {
            if (ModelState.IsValid)
            {


                
                var data = qLBanHangTheThaoEntities.Admins.Where(s => s.Email.Equals(email) && s.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["Password"] = data.FirstOrDefault().Password;

                    return RedirectToAction("Sanpham","Sanpham");
                }
               
            }
            return View();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}