using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoe.Models;

namespace WebShoe.Controllers
{
    public class DangKiController : Controller
    {
        CT25Team25Entities db = new CT25Team25Entities(); 
        // GET: DangKi
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            
            KhachHang kh = new KhachHang() {
                USERNAME = fc["Username"],
                DiaChi = fc["DiaChi"],
                PASSWORD = fc["Password"],
                NgaySinh = Convert.ToDateTime(fc["Ngaysinh"]),
                EMAIL = fc["Email"],
                TenKH = fc["TenKH"],
                PHONE = fc["phone"],
                GioiTinh = fc["gioitinh"],
            };
            ViewBag.Success = "Đăng kí thành công";
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return RedirectToAction("Index","DangKi");
        }
    }
}