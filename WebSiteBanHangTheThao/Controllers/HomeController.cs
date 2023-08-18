using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangTheThao.Context;
using WebSiteBanHangTheThao.Models;
using System.Data.Entity;
namespace WebSiteBanHangTheThao.Controllers
{
    public class HomeController : Controller
    {
        QLBanHangTheThaoEntities qLBanHangTheThaoEntities = new QLBanHangTheThaoEntities();
        public ActionResult Trangchu()
        {
            
            HomeModel homeModel = new HomeModel();
            homeModel.ListProduct = qLBanHangTheThaoEntities.Products.ToList();
            homeModel.ListCategory = qLBanHangTheThaoEntities.Categories.ToList();
           
            return View(homeModel);
        }

        
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangky(User dangky)
        {
            if (ModelState.IsValid)
            {
                var check = qLBanHangTheThaoEntities.Users.FirstOrDefault(s => s.Email == dangky.Email);
                if (check == null)
                {
                    dangky.Password = GetMD5(dangky.Password);
                    qLBanHangTheThaoEntities.Configuration.ValidateOnSaveEnabled = false;
                    qLBanHangTheThaoEntities.Users.Add(dangky);
                    qLBanHangTheThaoEntities.SaveChanges();
                    return RedirectToAction("Trangchu");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }
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


                var f_password = GetMD5(password);
                var data = qLBanHangTheThaoEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Trangchu");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Dangnhap");
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

        //Logout
        public ActionResult Dangxuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("Dangnhap");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}