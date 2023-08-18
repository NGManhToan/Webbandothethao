using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHangTheThao.Context;
using WebSiteBanHangTheThao.Models;

namespace WebSiteBanHangTheThao.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        QLBanHangTheThaoEntities qLBanHangTheThaoEntities = new QLBanHangTheThaoEntities();
        public ActionResult Dangnhap()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.username = Request.Cookies["username"].Value;
                ViewBag.password = Request.Cookies["password"].Value;
            }
            return View();
        }

        public void ghinhotaikhoan(string username, string password)
        {
            HttpCookie us = new HttpCookie("username");
            HttpCookie pas = new HttpCookie("password");

            us.Value = username;
            pas.Value = password;

            us.Expires = DateTime.Now.AddDays(1);
            pas.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(us);
            Response.Cookies.Add(pas);

        }

        [HttpPost]
        public ActionResult Login(string username, string password, string ghinho)
        {
            if (Request.Cookies["username"] != null && Request.Cookies["username"] != null)
            {
                username = Request.Cookies["username"].Value;
                password = Request.Cookies["password"].Value;
            }





            if (checkpassword(username, password))
            {
                var userSession = new UserLogin();
                userSession.Email = username;

                var listGroups = GetListGroupID(username);//Có thể viết dòng lệnh lấy các GroupID từ CSDL, ví dụ gán ="ADMIN", dùng List<string>

                Session.Add("SESSION_GROUP", listGroups);
                Session.Add("USER_SESSION", userSession);

                if (ghinho == "on")//Ghi nhớ
                    ghinhotaikhoan(username, password);
                return Redirect("~/Home/Index");

            }
            return Redirect("~/dang-nhap");
        }
        public List<string> GetListGroupID(string userName)
        {
            // var user = db.User.Single(x => x.UserName == userName);

            var data = (from a in qLBanHangTheThaoEntities.UserGroups
                        join b in qLBanHangTheThaoEntities.Users on a.ID equals b.GroupId
                        where b.Email == userName

                        select new
                        {
                            UserGroupID = b.GroupId,
                            UserGroupName = a.Name
                        });

            return data.Select(x => x.UserGroupName).ToList();

        }
        public bool checkpassword(string username, string password)
        {
            if (qLBanHangTheThaoEntities.Users.Where(x => x.Email == username && x.Password == password).Count() > 0)

                return true;
            else
                return false;


        }




        public ActionResult SignOut()
        {

            Session["USER_SESSION"] = null;




            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                HttpCookie us = Request.Cookies["username"];
                HttpCookie ps = Request.Cookies["password"];

                ps.Expires = DateTime.Now.AddDays(-1);
                us.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(us);
                Response.Cookies.Add(ps);
            }

            return Redirect("~/dang-nhap");
        }


        [ChildActionOnly]
        public ActionResult thongtindangnhap()
        {
            return PartialView("ThongTinDangNhap");

        }
    }
}