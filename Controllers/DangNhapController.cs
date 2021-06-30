using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShoe.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace WebShoe.Controllers
{
    public class DangNhapController : Controller
    {
       CT25Team25Entities db;
        public DangNhapController()
        {
            db = new CT25Team25Entities();
        }
        // GET: DangNhap
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index(Models.KhachHang model)
        {
            using (var context = new CT25Team25Entities())
            {
                var account = context.KhachHangs.Where(acc => acc.USERNAME == model.USERNAME && acc.PASSWORD == model.PASSWORD).FirstOrDefault();
                bool isValid = context.KhachHangs.Any(x => x.USERNAME == model.USERNAME
                && x.PASSWORD == model.PASSWORD);
                if (isValid)
                {
                    Session["MaKH"] = account.MaKH;
                    Session["TenKH"] = account.TenKH;
                    Session["USERNAME"] = account.USERNAME;
                    Session["PASSWORD"] = account.PASSWORD;
                    FormsAuthentication.SetAuthCookie(model.USERNAME, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid username or password !!!");
            Session["Message"] = "Sai thông tin tài khoản hoặc mật khẩu !!!";
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}